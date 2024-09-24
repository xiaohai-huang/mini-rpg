using System;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;
using ReactUnity.Helpers;
using ReactUnity.Scripting;

namespace Core.Game.Express
{
    public struct RequestCtx
    {
        public Dictionary<string, Value> body;
    }

    public class Value
    {
        private readonly object _value;
        private readonly IJavaScriptEngine _engine;

        public Value(object value, IJavaScriptEngine engine)
        {
            _value = value;
            _engine = engine;
        }

        public T[] ToArray<T>()
        {
            var array = _engine.TraverseScriptArray(_value).ToArray();
            T[] result = new T[array.Length];

            // Handle float case
            if (typeof(T) == typeof(float))
            {
                for (int i = 0; i < result.Length; i++)
                {
                    result[i] = (T)(object)Convert.ToSingle(array[i]); // Use Convert instead of Parse for a more direct conversion
                }
            }
            // Handle string case
            else if (typeof(T) == typeof(string))
            {
                for (int i = 0; i < result.Length; i++)
                {
                    result[i] = (T)(object)(array[i]?.ToString() ?? string.Empty); // Null-safe ToString conversion
                }
            }
            else
            {
                throw new InvalidOperationException($"Unsupported type {typeof(T)}");
            }

            return result;
        }

        public override string ToString()
        {
            return _value.ToString();
        }

        public int ToInt()
        {
            return Convert.ToInt32(_value);
        }

        public float ToFloat()
        {
            return Convert.ToSingle(_value);
        }

        public bool ToBoolean()
        {
            return Convert.ToBoolean(_value);
        }
    }

    public class Response<T>
    {
        public T data;
        public string message;
        public bool error;
    }

    public class Response
    {
        public string message;
        public bool error;
    }

    public class ResponseCtx
    {
        public Action<string> OnEnd;

        public void end<T>(Response<T> response)
        {
            string json = JsonConvert.SerializeObject(response);
            // pass to frontend
            OnEnd.Invoke(json);
        }

        public void end(Response response)
        {
            string json = JsonConvert.SerializeObject(response);
            // pass to frontend
            OnEnd.Invoke(json);
        }

        public void end(string message)
        {
            var resp = new Response() { message = message };
            end(resp);
        }
    }

    public class App
    {
        private readonly Dictionary<string, Action<RequestCtx, ResponseCtx>> _callbacks = new();
        private Callback _onAddRoute;
        private readonly IJavaScriptEngine _engine;

        public App(IJavaScriptEngine engine)
        {
            _engine = engine;
        }

        public void Init(object onAddRoute)
        {
            _onAddRoute = Callback.From(onAddRoute);
        }

        public void GET(string path, Action<RequestCtx, ResponseCtx> callback)
        {
            string key = GenKey("GET", path);
            _callbacks[key] = callback;
            _onAddRoute.Call("GET", path);
        }

        public void POST(string path, Action<RequestCtx, ResponseCtx> callback)
        {
            string key = GenKey("POST", path);
            _callbacks[key] = callback;
            _onAddRoute.Call("POST", path);
        }

        public void Handle(string method, string path, object body, object end)
        {
            Dictionary<string, Value> json = null;
            if (_engine.IsScriptObject(body))
            {
                json = new();
                var entries = _engine.TraverseScriptObject(body);
                while (entries.MoveNext())
                {
                    json.Add(entries.Current.Key, new Value(entries.Current.Value, _engine));
                }
            }

            var endCallback = Callback.From(end);
            string key = GenKey(method, path);

            _callbacks[key]
                .Invoke(
                    new RequestCtx() { body = json },
                    new ResponseCtx() { OnEnd = (message) => endCallback.Call(message) }
                );
        }

        private static string GenKey(string method, string path) => $"/{method}/-{path}";
    }
}
