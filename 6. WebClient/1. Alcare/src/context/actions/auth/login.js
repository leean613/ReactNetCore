import { authenticationService } from "../../../services/Login/AuthenticationService";
import {
  LOGIN_LOADING,
  LOGIN_SUCCESS,
  LOGIN_FAILED,
} from "../../constants/actionTypes/auth";

const login = (username, password) => async (dispatch) => {
  dispatch({
    type: LOGIN_LOADING,
  });

  try {
    var response = await authenticationService.login(username, password);
    if (response) {
      localStorage.setItem("currentUser", JSON.stringify(response.data.result));
      localStorage.setItem("token", response.data.result.accessToken);
      dispatch({
        type: LOGIN_SUCCESS,
        payload: response.result,
      });
    }
  } catch (error) {
    console.log(error);
    if (error !== undefined) {
      dispatch({
        type: LOGIN_FAILED,
        payload: error.data,
      });
    } else {
      dispatch({
        type: LOGIN_FAILED,
        payload: { errorMessage: "Cannot connect to server" },
      });
    }
  }
};

export default login;
