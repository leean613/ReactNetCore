import axios from "axios";
import { BASE_URL_API } from "./env.js";

axios.interceptors.response.use(
    response => checkStatus(response),
    error => Promise.reject(checkStatus(error.response))
);

export const callApi = (
    endpoint,
    method = "GET",
    body,
    headers = { "Content-Type": "application/json" }
) => {
    return axios({
        method,
        url: `${endpoint}`,
        baseURL: BASE_URL_API,
        headers: { ...headers, Accept: "application/json" },
        data: body
    })
};

export const callAuthorizationApi = (
    endpoint,
    method = "GET",
    body,
    headers = { "Content-Type": "application/json" }
) => {
    const token = localStorage.getItem("token");
    return callApi(endpoint, method, body, {
        ...headers,
        Authorization: `Bearer ${token}`
    });
};

export const callAuthorizationApiForm = (
    endpoint,
    method = "GET",
    body,
    headers = { "Content-Type": "application/x-www-form-urlencoded" }
) => {
    const token = localStorage.getItem("token");
    return callApi(endpoint, method, body, {
        ...headers,
        Authorization: `Bearer ${token}`
    });
};

function checkStatus(response) {
    // if (response.status >= 200 && response.status < 300) {
    //     return response;
    // }
    if (response === undefined)
        return response

    if (response.status === 401) {
        logout();
        window.location.reload();
    }
    return response;
}

const logout = () => {
    // remove user from local storage to log user out
    localStorage.removeItem('currentUser');
    localStorage.removeItem('token');
}