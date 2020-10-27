import { callAuthorizationApiForm } from "../../utils/apiCaller";

const UploadFileS3 = async (file) => {
    try {
        var response = await callAuthorizationApiForm(`api/Upload/UploadFileImage`, "POST", file);
        if (response) {
            return response;
        }
    } catch (error) {
        console.log(error.message);
    }
}

export const uploadService = {
    UploadFileS3
};