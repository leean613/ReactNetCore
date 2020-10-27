import { callApi } from "../../utils/apiCaller";

const login = async (username, password) => {
    try {
        const params = { username: username, password: password };
        var response = await callApi("api/glotech/login", "POST", params);
        console.log(response)
        if (response) {
            localStorage.setItem('currentUser', JSON.stringify(response.data.result));
            localStorage.setItem('token', response.data.result.accessToken);
            return response;
        }
    } catch (error) {
        console.log(error);
    }
}

const logout = () => {
    // remove user from local storage to log user out
    localStorage.removeItem('currentUser');
    localStorage.removeItem('token');
}

export const authenticationService = {
    login,
    logout,
    get token() {
        const token = localStorage.getItem('token') ? localStorage.getItem('token') : null;
        return token
    }
};
