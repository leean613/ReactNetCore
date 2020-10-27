import React, { useEffect, useState } from "react";
import "./../../assets/css/Users/createUser.scss"
// reactstrap components
import {
    Button,
    Card,
    CardHeader,
    CardBody,
    CardTitle,
    FormGroup,
    Form,
    Input,
    Row,
    Col,
} from "reactstrap";
import DropdownList from "../../components/common/DropdownList/DropdownList.js";
import { RingLoader } from "react-spinners";
import { useFormik } from "formik";
import { userService } from "./../../services/Users/UserService.js";

function UpdateUser(props) {
    const Roles = [
        { label: "Admin", value: 1 },
        { label: "Nhân viên", value: 0 },
    ];

    const { id } = props.match.params;
    const [role, setRole] = useState();
    const [isLoading, setIsLoading] = useState(false);
    const [isCreateSuccess, setIsCreateSuccess] = useState(false);
    const [isCreateFailed, setIsCreateFailed] = useState(false);
    const [userCode, setUserCode] = useState();
    const [userName, setUserName] = useState();

    useEffect(() => {
        const fetch = async () => {
            try {
                setIsLoading(true);
                var { data } = await userService.getUserByID(id);
                setUserCode(data.staff_Id);
                setUserName(data.staff_Name);
                setRole(data.admin_Flag);
                setIsLoading(false);
            } catch (error) {
                setIsLoading(false);
            }
        }
        fetch();
    }, [id]);

    const handleDropdownListOnChange = (option, controlName) => {
        setRole(option.value);
    }

    const handleOnsubmit = async (values) => {
        try {
            setIsLoading(true);
            var response = await userService.updateUser({
                Staff_Id: userCode,
                Admin_Flag: role
            });
            if (!response) {
                setIsCreateFailed(true);
                setIsCreateSuccess(false)
            } else {
                setIsCreateSuccess(true);
                setIsCreateFailed(false);
            }
            setIsLoading(false);
        } catch (error) {
            setIsLoading(false);
        }

    }

    const formik = useFormik({
        initialValues: {
            userCode: "",
            userName: ""
        },
        onSubmit: values => {
            handleOnsubmit(values);
        }
    });

    return (
        <div className="content">
            <Card className={`card-user card-user-create ${isLoading ? "common-wrapper-opacity" : ""}`}>
                <div className={`common-spinner ${isLoading ? "" : "common-spinner-isVisable"}`}>
                    <RingLoader></RingLoader>
                </div>
                <CardHeader>
                    <CardTitle tag="h5">Update User</CardTitle>
                </CardHeader>
                {isCreateSuccess && <label className="lb-messageSuccess">Success!</label>}
                {isCreateFailed && <label className="lb-messageFailed">Update user failed!</label>}
                <CardBody>
                    <Form onSubmit={formik.handleSubmit}>
                        <Row>
                            <Col md="12">
                                <FormGroup>
                                    <label>User code</label>
                                    <Input
                                        placeholder="Email"
                                        type="text"
                                        name="userCode"
                                        value={userCode}
                                        onChange={formik.handleChange}
                                        disabled
                                    />
                                </FormGroup>
                            </Col>
                        </Row>
                        <Row>
                            <Col md="12">
                                <FormGroup>
                                    <label>User Name</label>
                                    <Input
                                        placeholder="User Name"
                                        type="text"
                                        name="userName"
                                        value={userName}
                                        onChange={formik.handleChange}
                                        disabled
                                    />
                                </FormGroup>
                            </Col>
                        </Row>
                        <Row>
                            <Col md="12">
                                <label>Role</label>
                                <DropdownList
                                    className="react-select"
                                    classNamePrefix="react-select"
                                    placeholder="Roles"
                                    currentValue={role}
                                    handleDropdownListOnChange={handleDropdownListOnChange}
                                    Roles={Roles}
                                />
                            </Col>
                        </Row>
                        <Row>
                            <div className="update ml-auto mr-auto">
                                <Button
                                    className="btn-round"
                                    color="primary"
                                    type="submit"
                                >
                                    UPDATE
                                </Button>
                            </div>
                        </Row>
                    </Form>
                </CardBody>
            </Card>
        </div >
    );
}

export default UpdateUser;