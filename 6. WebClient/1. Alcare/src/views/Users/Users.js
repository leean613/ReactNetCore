import React, { useState, useEffect, useCallback, useRef } from 'react';
import { Link } from 'react-router-dom'
import { format } from 'date-fns'
// reactstrap components
import {
    Card,
    CardHeader,
    CardBody,
    CardTitle,
    Table,
    Button,
    Input,
} from "reactstrap";

import PaginationComponent from "../../components/common/Pagination/PaginationComponent";
import "./../../assets/css/Users/user.css";
import { userService } from "./../../services/Users/UserService.js";
import SweetAlert from 'react-bootstrap-sweetalert';
import { RingLoader } from "react-spinners";
import useCurrent from "../../utils/useCurrent.js";

const Tables = (props) => {

    const [page, setPage] = useState(1)
    const [total, setTotal] = useState(12)
    const [limit, setLimit] = useState(10)
    const [users, setUsers] = useState([])
    const [searchTerm, setSearchTerm] = useState('')
    const [deleteAlert, setDeleteAlert] = useState(null)
    const [isLoading, setIsLoading] = useState(false);
    const searchText = useRef("");
    const isCurrent = useCurrent();

    const fetch = useCallback(async () => {
        try {
            setIsLoading(true);
            var { data } = await userService.getUsersByPage(page, limit, searchTerm.trim());
            if (isCurrent.current === true) {
                setUsers(data.result.items);
                // setTotal(data.result[0] ? data.result[0].staff_Total : 0);
                setTotal(data.result.totalCount);
                setIsLoading(false);
            }

        } catch (error) {
            if (isCurrent.current === true) {
                console.log('Fail request get list user!');
                setIsLoading(false);
            }
        }
    }, [page, limit, searchTerm])

    useEffect(() => {
        fetch();
    }, [fetch])

    const toggleDeleteUser = (user) => {
        const getAlert = () => {
            return (
                <SweetAlert
                    warning
                    showCancel
                    confirmBtnText="Yes, delete it!"
                    confirmBtnBsStyle="danger"
                    title="Are you sure?"
                    onConfirm={() => deleteUser(user.id)}
                    onCancel={() => hideDeleteAlert()}
                    focusCancelBtn
                >
                    You will not be able to recover this user!
                </SweetAlert>
            )
        }

        setDeleteAlert(getAlert());
    }

    const deleteUser = async (userID) => {
        try {
            await userService.deleteUser(userID);
            setDeleteAlert(null);
            fetch();
        } catch (error) {

        }
    }

    const hideDeleteAlert = () => {
        setDeleteAlert(null);
    }

    const onSearch = () => {
        setSearchTerm(searchText.current);
    }

    const handleOnChangeSearch = (event) => {
        searchText.current = event.target.value;
    }

    const handleOnKeyPressSearch = (event) => {
        if (event.which === 13) {
            onSearch();
        }
    }

    return (
        <>
            <div className="content">
                {deleteAlert}
                <Card className={`${isLoading ? "common-wrapper-opacity" : ""}`}>
                    <div className={`common-spinner ${isLoading ? "" : "common-spinner-isVisable"}`}>
                        <RingLoader></RingLoader>
                    </div>
                    <CardHeader>
                        <div className="d-flex justify-content-between mb-2">
                            <CardTitle tag="h4">Users Table</CardTitle>
                            <div className="d-flex">
                                <div className="d-flex align-items-center position-relative mr-2">
                                    <Input
                                        placeholder="Search"
                                        type="text"
                                        className="input-search"
                                        onChange={(event) => handleOnChangeSearch(event)}
                                        onKeyPress={(event) => handleOnKeyPressSearch(event)}
                                    />
                                    <span className="search-icon" onClick={() => onSearch()}>
                                        <i className="simple-icon-magnifier fs-20" />
                                    </span>
                                </div>
                                <Button tag={Link} to="/admin/users/create" className="btn-createUser">
                                    Create User
                                </Button>
                            </div>
                        </div>

                    </CardHeader>
                    <CardBody>
                        <Table>
                            <thead className="text-primary">
                                <tr>
                                    <th className="th-username">User Name</th>
                                    <th className="th-registerDate">Registration Date</th>
                                    <th className="th-adminFlag">Admin</th>
                                    <th className="th-action">Action</th>
                                </tr>
                            </thead>
                            <tbody>
                                {users.map((user, key) => {
                                    return (
                                        <tr key={user.id}>
                                            <td>{user.userName}</td>
                                            <td>{format(new Date(user.createDate), "dd/MM/yyyy")}</td>
                                            <td className="td-adminFlag">
                                                <i className={`fas fa-check i-adminFlag ${user.role === 1 ? "" : "noneVisible"}`}></i>
                                            </td>
                                            <td>
                                                <Link to={''}>
                                                    <Button tag={Link} to={`/admin/users/${user.id}`} color="primary" outline size="xs" className="default mr-2">
                                                        Detail
                                                    </Button>
                                                </Link>
                                                {/* <Button
                                                    color="success"
                                                    outline
                                                    size="xs"
                                                    className="default mr-2"
                                                    onClick={() => toggleBlockUser(user)}
                                                >
                                                    {user.blockedAt ? 'UnBlock' : 'Block'}
                                                Block
                                                </Button> */}
                                                <Button color="danger" outline size="xs" className="default"
                                                    onClick={() => toggleDeleteUser(user)}
                                                >
                                                    Delete
                                                </Button>
                                            </td>
                                        </tr>
                                    )
                                })}

                            </tbody>
                        </Table>
                        <PaginationComponent page={page} limit={limit} total={total} onChangePage={(page) => setPage(page)} />
                    </CardBody>
                </Card>
            </div>
        </>
    );
}

export default Tables;
