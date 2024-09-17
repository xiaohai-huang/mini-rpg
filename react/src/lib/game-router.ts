import Router from "router";
import { Bridge } from "./unity-objects";

const router = Router();

function addRoute(method: string, path: string) {
  router[method.toLowerCase()](path, (req, res) => {
    Bridge.app.Handle(method, path, req.body, res.end);
  });
}

Bridge.CreateRouter(addRoute);

function request(req) {
  return new Promise((resolve, reject) => {
    router(
      req,
      {
        end: (message: string) => {
          resolve(message);
        },
      },
      (err) => {
        console.error(err);
        reject(err);
      }
    );
  });
}

class Agent {
  async get(path: string) {
    return await request({ method: "GET", url: path });
  }
  async post(path: string, body: any = null) {
    return await request({
      method: "POST",
      url: path,
      body,
    });
  }
}

export const agent = new Agent();

export default router;
