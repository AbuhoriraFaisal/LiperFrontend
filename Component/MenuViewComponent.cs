
//using Microsoft.AspNetCore.Http;
//using Microsoft.AspNetCore.Mvc;
//using System.Web.Mvc;

//namespace LiperFrontend.Component
//{
//    public class MenuViewComponent : ViewComponent
//    {
//        private readonly IGenericRepositroy<RolePermissions> rolePermissions;

//        public MenuViewComponent(IGenericRepositroy<RolePermissions> rolePermissions)
//        {
//            this.rolePermissions = rolePermissions;
//        }

//        public IViewComponentResult Invoke()
//        {
//            List<RolePermissions> rPermissions = new List<RolePermissions>();
//            try
//            {
//                if (HttpContext.Session.GetInt32("UserRoleId") != null)
//                {
//                    rPermissions = rolePermissions.GetValidData().Distinct().ToList();
//                    int roleId = int.Parse(HttpContext.Session.GetInt32("UserRoleId").ToString());
//                    rPermissions=rPermissions.Where(s => s.RoleId == roleId).ToList();
//                }
//                if (rPermissions.Where(s => s.Controller == "Role" && s.Action == "Index").ToList().Count > 0)
//                {
//                    ViewBag.Roles = rPermissions.Where(s => s.Controller == "Role" );
//                }
//                if (rPermissions.Where(s => s.Controller == "User" && s.Action == "Index").ToList().Count>0)
//                {
//                    ViewBag.Users = rPermissions.Where(s => s.Controller == "User");
//                }
//                if(rPermissions.Where(s => s.Controller == "RolePermissions" && s.Action == "Index").ToList().Count > 0)
//                {
//                    ViewBag.RolePermission = rPermissions.Where(s => s.Controller == "RolePermissions");
//                }
//                return View(rPermissions);
//                //return View(rPermissions.OrderBy(s=>s.Controller));
//            }
//            catch (Exception)
//            {
//                return View(rPermissions);     
//            }

//        }
//    }
//}
