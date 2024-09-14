using System;
using System.Collections.Generic;
using ReactUnity.Helpers;

namespace Core.Game.Express
{
    public struct Request
    {
        public string body;
    }

    public struct Response
    {
        public Action<string> end;
    }

    public class App
    {
        private readonly Dictionary<string, Action<Request, Response>> _callbacks = new();
        private Callback _onAddRoute;

        public void Init(object onAddRoute)
        {
            _onAddRoute = Callback.From(onAddRoute);
        }

        public void GET(string path, Action<Request, Response> callback)
        {
            string key = GenKey("GET", path);
            _callbacks[key] = callback;
            _onAddRoute.Call("GET", path);
        }

        public void POST(string path, Action<Request, Response> callback)
        {
            string key = GenKey("POST", path);
            _callbacks[key] = callback;
            _onAddRoute.Call("POST", path);
        }

        public void Handle(string method, string path, string body, object end)
        {
            var endCallback = Callback.From(end);
            string key = GenKey(method, path);

            _callbacks[key]
                .Invoke(
                    new Request() { body = body },
                    new Response() { end = (message) => endCallback.Call(message) }
                );
        }

        private static string GenKey(string method, string path) => $"/{method}/-{path}";
    }
}
