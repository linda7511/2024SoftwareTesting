import { get,post } from './axiosConfig.js'

export function getRootInfo() {
    get('/api/Employee/Login/zbh/123456', {})
        .then(response => {
            console.log(response);
            console.log(typeof(response));
            if (response.token) {
                localStorage.setItem('token', response.token);
            }      
        })
        .catch(error => {
            console.error(error);
            return null;
        })
}
