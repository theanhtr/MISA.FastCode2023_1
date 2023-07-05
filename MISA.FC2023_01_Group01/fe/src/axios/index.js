import axios from "axios";
import { success, failure } from "../service/HttpResponse.js";

const MISAAxios = axios.create({
  baseURL: "https://f64a-210-245-52-125.ngrok-free.app/api/",
  headers: { Accept: "application/json", "Content-Type": "application/json" },
});

MISAAxios.interceptors.response.use(
  function (response) {
    return success(response);
  },
  function (error) {
    return failure(error.response);
  }
);

export default MISAAxios;
