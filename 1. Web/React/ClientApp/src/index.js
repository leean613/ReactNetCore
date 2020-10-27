import React from "react";
import ReactDOM from "react-dom";

import "bootstrap/dist/css/bootstrap.css";
import "./assets/scss/paper-dashboard.scss";
import "./assets/common/common.css";
import "perfect-scrollbar/css/perfect-scrollbar.css";
import App from "./components/App/App";

// import { configureFakeBackend } from './common/Helpers';
// configureFakeBackend();

ReactDOM.render(
    <App></App>,
    document.getElementById("root")
);