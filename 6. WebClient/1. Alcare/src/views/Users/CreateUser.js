import React, { useState } from "react";
import "./../../assets/css/Users/createUser.scss";
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
import * as Yup from "yup";
import { userService } from "./../../services/Users/UserService.js";

function CreateUser(props) {
  const Roles = [
    { label: "Admin", value: 1 },
    { label: "Nhân viên", value: 0 },
  ];

  const [role, setRole] = useState(0);

  const [isLoading, setIsLoading] = useState(false);

  const [isCreateSuccess, setIsCreateSuccess] = useState(false);
  const [isCreateFailed, setIsCreateFailed] = useState(false);

  const handleDropdownListOnChange = (option, controlName) => {
    setRole(option.value);
  };

  const handleOnsubmit = async (values) => {
    try {
      setIsLoading(true);
      var response = await userService.createUser({
        userName: values.userName,
        role: role,
      });

      if (!response) {
        setIsCreateFailed(true);
        setIsCreateSuccess(false);
      } else {
        setIsCreateSuccess(true);
        setIsCreateFailed(false);
      }
      setIsLoading(false);
    } catch (error) {
      setIsLoading(false);
    }
  };

  const formik = useFormik({
    initialValues: {
      userName: "",
    },
    validationSchema: Yup.object({
      userName: Yup.string().required("Please enter user name!"),
    }),
    onSubmit: (values) => {
      handleOnsubmit(values);
    },
  });

  return (
    <div className="content">
      <Card
        className={`card-user card-user-create ${
          isLoading ? "common-wrapper-opacity" : ""
        }`}
      >
        <div
          className={`common-spinner ${
            isLoading ? "" : "common-spinner-isVisable"
          }`}
        >
          <RingLoader></RingLoader>
        </div>
        <CardHeader>
          <CardTitle tag="h5">Create User</CardTitle>
        </CardHeader>
        {isCreateSuccess && (
          <label className="lb-messageSuccess">Success!</label>
        )}
        {isCreateFailed && (
          <label className="lb-messageFailed">Create user failed!</label>
        )}

        {isCreateFailed && <label className="lb-messageFailed">{}</label>}
        <CardBody>
          <Form onSubmit={formik.handleSubmit}>
            {/* <Row>
                            <Col md="12">
                                <FormGroup>
                                    <label>User code</label>
                                    <Input
                                        placeholder="User code"
                                        type="text"
                                        name="userCode"
                                        value={formik.values.userCode}
                                        onChange={formik.handleChange}
                                    />
                                    {formik.errors.userCode && formik.touched.userCode && (
                                        <p className="lb-loginFailed">{formik.errors.userCode}</p>
                                    )}
                                </FormGroup>
                            </Col>
                        </Row> */}
            <Row>
              <Col md="12">
                <FormGroup>
                  <label>User Name</label>
                  <Input
                    placeholder="User Name"
                    type="text"
                    name="userName"
                    value={formik.values.userName}
                    onChange={formik.handleChange}
                  />
                  {formik.errors.userName && formik.touched.userName && (
                    <p className="lb-loginFailed">{formik.errors.userName}</p>
                  )}
                </FormGroup>
              </Col>
            </Row>
            {/* <Row>
                            <Col md="12">
                                <FormGroup>
                                    <label>First Name</label>
                                    <Input
                                        defaultValue=""
                                        placeholder="First Name"
                                        type="text"
                                    />
                                </FormGroup>
                            </Col>
                        </Row>
                        <Row>
                            <Col md="12">
                                <FormGroup>
                                    <label>Last Name</label>
                                    <Input
                                        defaultValue=""
                                        placeholder="Last Name"
                                        type="text"
                                    />
                                </FormGroup>
                            </Col>
                        </Row> */}
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
            {/* <Row>
                            <Col md="12">
                                <FormGroup>
                                    <label>Description</label>
                                    <Input
                                        type="textarea"
                                        defaultValue=""
                                        placeholder="Description"
                                    />
                                </FormGroup>
                            </Col>
                        </Row> */}
            <Row>
              <div className="update ml-auto mr-auto">
                <Button className="btn-round" color="primary" type="submit">
                  CREATE
                </Button>
              </div>
            </Row>
          </Form>
        </CardBody>
      </Card>
    </div>
  );
}

export default CreateUser;
