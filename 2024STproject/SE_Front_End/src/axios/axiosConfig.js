import axios from "axios";

const instance = axios.create({
    baseURL: '',
    timeout: 5000,
});

export var userName = "";
export var userId = 0;

instance.interceptors.request.use(
    config => {
        return config;
    },
    error => {
        return Promise.reject(error);
    }
);

instance.interceptors.response.use(
    response => {
        return response.data;
    },
    error => {
        return Promise.reject(error);
    }
);

export function post(url, data) {
    return instance.post(url, data);
}

export function get(url, params) {
    return instance.get(url, {
        params: params
    });
}

export function put(url, data) {
    return instance.put(url, data);
}

export function del(url, data) {
    return instance.delete(url, {
        data: data
    });
}

export function setUserName(data) {
    userName = data;
}

export function setUserId(data) {
    userId = data;
}

export function getUserInfo() {
    return  JSON.parse(localStorage.getItem('userInfo'));
}