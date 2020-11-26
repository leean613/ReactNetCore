import React, { Component, Fragment } from "react";
import { PaginationItem, Pagination, PaginationLink } from "reactstrap";

import "./../../../assets/css/Pagination/pagination.scss";

export default class PaginationComponent extends Component {
    render() {
        const { page, limit, total, onChangePage } = this.props;
        const lastPage = Math.ceil(total / limit);

        return (
            <Fragment>
                <Pagination size="sm" aria-label="Page navigation example" listClassName="justify-content-end">
                    <PaginationItem onClick={() => onChangePage(1)}>
                        <PaginationLink className="first">
                            <i className="simple-icon-control-start" />
                        </PaginationLink>
                    </PaginationItem>
                    <PaginationItem onClick={() => onChangePage(page === 1 ? 1 : page - 1)}>
                        <PaginationLink className="prev">
                            <i className="simple-icon-arrow-left" />
                        </PaginationLink>
                    </PaginationItem>
                    {page === lastPage && page >= 3 && (
                        <PaginationItem onClick={() => onChangePage(page - 2)}>
                            <PaginationLink>{page - 2}</PaginationLink>
                        </PaginationItem>
                    )}
                    {page !== 1 && (
                        <PaginationItem onClick={() => onChangePage(page - 1)}>
                            <PaginationLink>{page - 1}</PaginationLink>
                        </PaginationItem>
                    )}
                    <PaginationItem active>
                        <PaginationLink>{page}</PaginationLink>
                    </PaginationItem>
                    {page <= lastPage - 1 && (
                        <PaginationItem onClick={() => onChangePage(page + 1)}>
                            <PaginationLink>{page + 1}</PaginationLink>
                        </PaginationItem>
                    )}
                    {page === 1 && page <= lastPage - 2 && (
                        <PaginationItem onClick={() => onChangePage(page + 2)}>
                            <PaginationLink>{page + 2}</PaginationLink>
                        </PaginationItem>
                    )}
                    <PaginationItem onClick={() => onChangePage(page === lastPage ? lastPage : page + 1)}>
                        <PaginationLink className="next">
                            <i className="simple-icon-arrow-right" />
                        </PaginationLink>
                    </PaginationItem>
                    <PaginationItem onClick={() => onChangePage(lastPage)}>
                        <PaginationLink className="last">
                            <i className="simple-icon-control-end" />
                        </PaginationLink>
                    </PaginationItem>
                </Pagination>
            </Fragment>
        );
    }
}
