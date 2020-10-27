import React from "react";
import PerfectScrollbar from "perfect-scrollbar";
import TopNav from "./../components/Navbars/TopNav.js";
import Sidebar from "./../components/Sidebar/Sidebar.js";
import { AuthorizationRoutes } from "../components/common/Routes/SidebarRoutes.js";
import RouteConfiguration from "../components/common/Routes/RouteConfiguration.js";

var ps;


class Dashboard extends React.Component {
  constructor(props) {
    super(props);
    this.state = {
      backgroundColor: "black",
      activeColor: "info",
    };
    this.mainPanel = React.createRef();
  }
  componentDidMount() {
    if (navigator.platform.indexOf("Win") > -1) {
      ps = new PerfectScrollbar(this.mainPanel.current);
      document.body.classList.toggle("perfect-scrollbar-on");
    }
  }
  componentWillUnmount() {
    if (navigator.platform.indexOf("Win") > -1) {
      ps.destroy();
      document.body.classList.toggle("perfect-scrollbar-on");
    }
  }
  componentDidUpdate(e) {
    if (e.history.action === "PUSH") {
      this.mainPanel.current.scrollTop = 0;
      document.scrollingElement.scrollTop = 0;
    }
  }
  handleActiveClick = (color) => {
    this.setState({ activeColor: color });
  };
  handleBgClick = (color) => {
    this.setState({ backgroundColor: color });
  };
  render() {
    const { role } = localStorage.getItem("currentUser") ? JSON.parse(localStorage.getItem("currentUser")) : { role: null };
    var routes = role === 1 ? AuthorizationRoutes.adminRoutes : AuthorizationRoutes.userRoutes;

    return (
      <div className="wrapper">
        <Sidebar
          {...this.props}
          routes={routes}
          bgColor={this.state.backgroundColor}
          activeColor={this.state.activeColor}
        />
        <div className="main-panel" ref={this.mainPanel}>
          <TopNav {...this.props} routes={routes} />
          <RouteConfiguration routes={routes} />
        </div>
      </div>
    );
  }
}

export default Dashboard;
