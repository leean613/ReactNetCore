import { callAuthorizationApi } from "../../utils/apiCaller";

const getUsersByPage = async (page, pageSize, searchTerm) => {
  try {
    var response = await callAuthorizationApi(
      `api/glotech/user/${page}/${pageSize}?searchTerm=${searchTerm}`,
      "GET",
      null
    );
    if (response) {
      return response;
    }
  } catch (error) {
    console.log(error.message);
  }
};

const createUser = async (user) => {
  try {
    var response = await callAuthorizationApi(`api/glotech/user`, "POST", user);
    console.log(response);
    if (response) {
      return response;
    }
  } catch (error) {}
};

const getUserByID = async (userID) => {
  try {
    var response = await callAuthorizationApi(
      `api/glotech/user/${userID}`,
      "GET",
      null
    );
    if (response) {
      console.log(response);
      return response;
    }
  } catch (error) {
    console.log(error);
  }
};

const updateUser = async (user) => {
  try {
    var response = await callAuthorizationApi(`api/glotech/user`, "PUT", user);
    if (response) {
      return response;
    }
  } catch (error) {
    console.log(error.message);
  }
};

const deleteUser = async (userID) => {
  try {
    var response = await callAuthorizationApi(
      `api/glotech/user/${userID}`,
      "DELETE",
      null
    );
    if (response) {
      return response;
    }
  } catch (error) {
    console.log(error.message);
  }
};

export const userService = {
  getUsersByPage,
  createUser,
  getUserByID,
  updateUser,
  deleteUser,
};
