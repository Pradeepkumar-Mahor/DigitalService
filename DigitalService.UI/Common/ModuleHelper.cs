using DigitalService.UI.Models;
using System;

namespace DigitalService.UI.Common
{
    /// <summary>
    /// This is where you customize the navigation sidebar
    /// </summary>
    public static class ModuleHelper
    {
        public enum Module
        {
            Home,

            About,

            Contact,

            Error,

            Login,

            Register,

            SuperAdmin,

            Role,

            UserLogs
        }

        public static SidebarMenu AddHeader(string name)
        {
            return new SidebarMenu
            {
                Type = SidebarMenuType.Header,
                Name = name,
            };
        }

        public static SidebarMenu AddTree(string name, string iconClassName = "bi bi-link")
        {
            return new SidebarMenu
            {
                Type = SidebarMenuType.Tree,
                IsActive = false,
                Name = name,
                IconClassName = iconClassName,
                URLPath = "#",
            };
        }

        public static SidebarMenu AddModule(Module module, Tuple<int, int, int> counter = null)
        {
            if (counter == null)
                counter = Tuple.Create(0, 0, 0);

            switch (module)
            {
                case Module.Home:
                    return new SidebarMenu
                    {
                        Type = SidebarMenuType.Link,
                        Name = "Home",
                        IconClassName = "bi bi-house-door-fill",
                        URLPath = "/",
                        LinkCounter = counter,
                    };

                case Module.Login:
                    return new SidebarMenu
                    {
                        Type = SidebarMenuType.Link,
                        Name = "Login",
                        IconClassName = "bi bi-door-open-fill",
                        URLPath = "/Account/Login",
                        LinkCounter = counter,
                    };

                case Module.Register:
                    return new SidebarMenu
                    {
                        Type = SidebarMenuType.Link,
                        Name = "Register",
                        IconClassName = "bi bi-person-fill-add",
                        URLPath = "/Account/Register",
                        LinkCounter = counter,
                    };

                case Module.About:
                    return new SidebarMenu
                    {
                        Type = SidebarMenuType.Link,
                        Name = "About",
                        IconClassName = "bi bi-people-fill",
                        URLPath = "/Home/About",
                        LinkCounter = counter,
                    };

                case Module.Contact:
                    return new SidebarMenu
                    {
                        Type = SidebarMenuType.Link,
                        Name = "Contact",
                        IconClassName = "bi bi-telephone-fill",
                        URLPath = "/Home/Contact",
                        LinkCounter = counter,
                    };

                case Module.Error:
                    return new SidebarMenu
                    {
                        Type = SidebarMenuType.Link,
                        Name = "Error",
                        IconClassName = "bi bi-exclamation-triangle-fill",
                        URLPath = "/Home/Error",
                        LinkCounter = counter,
                    };

                case Module.SuperAdmin:
                    return new SidebarMenu
                    {
                        Type = SidebarMenuType.Link,
                        Name = "User",
                        IconClassName = "bi bi-people-fill",
                        URLPath = "/SuperAdmin",
                        LinkCounter = counter,
                    };

                case Module.Role:
                    return new SidebarMenu
                    {
                        Type = SidebarMenuType.Link,
                        Name = "Role",
                        IconClassName = "bi bi-person-fill-gear",
                        URLPath = "/Role",
                        LinkCounter = counter,
                    };

                case Module.UserLogs:
                    return new SidebarMenu
                    {
                        Type = SidebarMenuType.Link,
                        Name = "UserLogs",
                        IconClassName = "bi bi-clock-history",
                        URLPath = "/UserLogs",
                        LinkCounter = counter,
                    };

                default:
                    break;
            }

            return null;
        }
    }
}