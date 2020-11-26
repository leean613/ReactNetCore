import React, { useState } from 'react';
import '../../assets/css/Login/login.css';
import { authenticationService } from "../../services/Login/AuthenticationService";
import { useFormik } from "formik";
import * as Yup from "yup";
import { RingLoader } from "react-spinners";

function Login(props) {

    if (authenticationService.token) {
        props.history.push('/admin/dashboard');
    }

    const [loginFailed, setloginFailed] = useState(false);
    const [isLoading, setIsLoading] = useState(false);

    const handleOnsubmit = async (values) => {
        setIsLoading(true);
        try {
            var response = await authenticationService.login(values.username, values.password);
            if (response) {
                props.history.push("/admin/dashboard");
            } else {
                setloginFailed(true);
            }
        } catch (error) {
            console.log(error);
        };
        setIsLoading(false);
    }

    const formik = useFormik({
        initialValues: {
            username: "",
            password: ""
        },
        validationSchema: Yup.object({
            username: Yup.string()
                .required("Please enter user name!"),
            password: Yup.string()
                .required('Please Enter your password!')
            // .matches(
            //     /^(?=.*[A-Za-z])(?=.*\d)(?=.*[@$!%*#?&])[A-Za-z\d@$!%*#?&]{8,}$/,
            //     "Must Contain 8 Characters, One Uppercase, One Lowercase, One Number and one special case Character!"
            // ),
        }),
        onSubmit: values => {
            handleOnsubmit(values);
        }
    });

    return (
        <div className="login">
            <div className={`login-spinner ${isLoading ? "" : "login-spinner-isVisable"}`}>
                <RingLoader></RingLoader>
            </div>
            <div className={`auth-wrapper ${isLoading ? "auth-wrapper-opacity" : ""}`}>
                <div className="auth-inner">
                    <form onSubmit={formik.handleSubmit}>
                        <h3>Sign In</h3>

                        {loginFailed &&
                            <label className="lb-loginFailed">User name or password is incorrect!</label>
                        }

                        <div className="form-group">
                            <label>User Name</label>
                            <input
                                type="text"
                                name="username"
                                value={formik.values.username}
                                className="form-control"
                                placeholder="Enter User name"
                                onChange={formik.handleChange}
                            />
                            {formik.errors.username && formik.touched.username && (
                                <p className="lb-loginFailed">{formik.errors.username}</p>
                            )}
                        </div>

                        <div className="form-group">
                            <label>Password</label>
                            <input
                                type="password"
                                name="password"
                                value={formik.values.password}
                                className="form-control"
                                placeholder="Enter password"
                                onChange={formik.handleChange}
                            />
                            {formik.errors.password && formik.touched.password && (
                                <p className="lb-loginFailed">{formik.errors.password}</p>
                            )}
                        </div>

                        < button
                            type="submit"
                            className="btn btn-primary btn-block"
                        >LOGIN
                        </button>
                    </form>
                </div>
            </div>
        </div>
    );
}

export default Login;