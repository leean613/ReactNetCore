import Dashboard from "../../../views/Dashboard.js";
import Notifications from "../../../views/Notifications.js";
import Icons from "../../../views/Icons.js";
import Typography from "../../../views/Typography.js";
import UserList from "../../../views/Users/Users.js";
import Maps from "../../../views/Map.js";
import UserPage from "../../../views/User.js";
import FileUploadComponent from "../../../views/Upload/Upload.js";

var adminRoutes = [
  {
    path: "/dashboard",
    name: "Dashboard",
    icon: "nc-icon nc-bank",
    component: Dashboard,
    layout: "/admin",
  },
  {
    path: "/icons",
    name: "Icons",
    icon: "nc-icon nc-diamond",
    component: Icons,
    layout: "/admin",
  },
  {
    path: "/maps",
    name: "Maps",
    icon: "nc-icon nc-pin-3",
    component: Maps,
    layout: "/admin",
  },
  {
    path: "/notifications",
    name: "Notifications",
    icon: "nc-icon nc-bell-55",
    component: Notifications,
    layout: "/admin",
  },
  {
    path: "/user-page",
    name: "User Profile",
    icon: "nc-icon nc-single-02",
    component: UserPage,
    layout: "/admin",
  },
  {
    path: "/users",
    name: "Users",
    icon: "nc-icon nc-tile-56",
    component: UserList,
    layout: "/admin",
  },
  {
    path: "/typography",
    name: "Typography",
    icon: "nc-icon nc-caps-small",
    component: Typography,
    layout: "/admin",
    },
    {
        path: "/upload",
        name: "Upload File",
        icon: "nc-icon nc-cloud-download-93",
        component: FileUploadComponent,
        layout: "/admin",
    }
];

var userRoutes = [
  {
    path: "/dashboard",
    name: "Dashboard",
    icon: "nc-icon nc-bank",
    component: Dashboard,
    layout: "/admin",
  },
  {
    path: "/user-page",
    name: "User Profile",
    icon: "nc-icon nc-single-02",
    component: UserPage,
    layout: "/admin",
  },
  {
    path: "/users",
    name: "Users",
    icon: "nc-icon nc-tile-56",
    component: UserList,
    layout: "/admin",
  },
  {
    path: "/typography",
    name: "Typography",
    icon: "nc-icon nc-caps-small",
    component: Typography,
    layout: "/admin",
  }
];

export const AuthorizationRoutes = {
  adminRoutes,
  userRoutes
};
