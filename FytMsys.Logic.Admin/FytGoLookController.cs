﻿using Domain.Entity;
using FytMsys.Common;
using FytMsys.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace FytMsys.Logic.Admin
{
    /// <summary>
    /// 去看看后台管理
    /// </summary>
    public class FytGoLookController : BaseController
    {
        #region 1、[去看看管理] 初始化 + Index()
        /// <summary>
        /// 初始化
        /// </summary>
        /// <returns></returns>
        public ActionResult Index()
        {
            return View();
        }
        #endregion

        #region 2、[去看看管理] 加载数据 + IndexData()
        /// <summary>
        /// 加载数据
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public ActionResult IndexData()
        {
            var jsonM = new JsonHelper.JsonAjaxModel() { Status = "y", Msg = "success" };
            try
            {
                var key = FytRequest.GetFormStringEncode("key"); //关键字查询         
                int pageSize = FytRequest.GetFormInt("pageSize", 1),
                    pageIndex = FytRequest.GetFormInt("pageIndex", 10);
                var lq = from gl in OperateSession.SetContext.lv_GoLook
                         orderby gl.UpdateTime descending
                         select new
                         {
                             gl.ID,
                             gl.Number,
                             gl.tb_User.NickName,
                             gl.tb_User.TrueName,
                             gl.Title,
                             gl.Rsum,
                             gl.Price,
                             gl.GoAddress,
                             gl.XcTime,
                             gl.ArriveTime,
                             gl.Flags,
                             gl.IsSpecial,
                             gl.Audit,
                             gl.UpdateTime
                         };
                if (key != "")
                {
                    lq = lq.Where(m => m.Title.Contains(key) || m.GoAddress.Contains(key));
                }
                jsonM.Data = lq.Skip(pageSize * (pageIndex - 1)).Take(pageSize).ToList();
                jsonM.PageRows = lq.Count();
                jsonM.PageTotal = Convert.ToInt32(Math.Ceiling((lq.Count() * 1.0) / pageSize));
            }
            catch (Exception ex)
            {
                jsonM.Status = "err";
                jsonM.Msg = "获取数据发生错误！ 消息：" + ex.Message;
                LogHelper.WriteLog(typeof(FytGoLookController), ex);
            }
            return MyJson(jsonM, "");
        }
        #endregion

        #region 3、[去看看管理] 删除记录 + DeleteBy()
        /// <summary>
        /// 删除记录
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public ActionResult DeleteBy()
        {
            var jsonM = new JsonHelper.JsonAjaxModel() { Status = "y", Msg = "success" };
            try
            {
                var ids = FytRequest.GetFormString("id");
                if (!ids.Contains(","))
                {
                    var id = Convert.ToInt32(ids);
                    OperateContext<lv_GoLookOrder>.SetServer.DeleteBy(m => m.LookId == id);
                    OperateContext<lv_GoLook>.SetServer.DeleteBy(m => m.ID == id);
                }
                else
                {
                    List<int> result = new List<string>(ids.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries)).ConvertAll(int.Parse);
                    OperateContext<lv_GoLookOrder>.SetServer.DeleteBy(m => result.Contains(m.LookId));
                    OperateContext<lv_GoLook>.SetServer.DeleteBy(m => result.Contains(m.ID));
                }
            }
            catch (Exception ex)
            {
                jsonM.Status = "err";
                jsonM.Msg = "删除数据发生错误！ 消息：" + ex.Message;
                LogHelper.WriteLog(typeof(FytGoLookController), ex);
            }
            return MyJson(jsonM, "");
        }
        #endregion

        #region 4、[去看看管理] 参与用户初始化 + UserIndex()
        /// <summary>
        /// 参与用户初始化
        /// </summary>
        /// <returns></returns>
        public ActionResult UserIndex()
        {
            return View();
        }
        #endregion

        #region 5、[去看看管理] 参与用户加载数据 + UserIndexData()
        /// <summary>
        /// 参与用户加载数据
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public ActionResult UserIndexData()
        {
            var jsonM = new JsonHelper.JsonAjaxModel() { Status = "y", Msg = "success" };
            try
            {
                var key = FytRequest.GetFormStringEncode("key"); //关键字查询         
                int lookId = FytRequest.GetFormInt("lookId"),//去看看ID
                    pageSize = FytRequest.GetFormInt("pageSize", 1),
                    pageIndex = FytRequest.GetFormInt("pageIndex", 10);
                var lq = from po in OperateSession.SetContext.lv_GoLookOrder
                         where po.LookId == lookId
                         orderby po.AddTime descending
                         select new
                         {
                             po.ID,
                             po.tb_User.NickName,
                             po.tb_User.TrueName,
                             po.PayPrice,
                             po.PayName,
                             po.PayStatus,
                             po.AddTime
                         };
                if (key != "")
                {
                    lq = lq.Where(m => m.TrueName.Contains(key));
                }
                jsonM.Data = lq.Skip(pageSize * (pageIndex - 1)).Take(pageSize).ToList();
                jsonM.PageRows = lq.Count();
                jsonM.PageTotal = Convert.ToInt32(Math.Ceiling((lq.Count() * 1.0) / pageSize));
            }
            catch (Exception ex)
            {
                jsonM.Status = "err";
                jsonM.Msg = "获取数据发生错误！ 消息：" + ex.Message;
                LogHelper.WriteLog(typeof(FytGoLookController), ex);
            }
            return MyJson(jsonM, "");
        }
        #endregion

        #region 6、[去看看管理] 去看看修改特色旅程 + ChangeIsSpecial()
        /// <summary>
        /// 去看看修改特色旅程
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public ActionResult ChangeIsSpecial()
        {
            var jsonM = new JsonHelper.JsonAjaxModel() { Status = "y", Msg = "success" };
            try
            {
                var ids = FytRequest.GetFormString("id");
                if (!ids.Contains(","))
                {
                    var id = Convert.ToInt32(ids);
                    var model = OperateContext<lv_GoLook>.SetServer.GetModel(m => m.ID == id);
                    if (model != null)
                    {
                        model.IsSpecial = !model.IsSpecial;
                        OperateContext<lv_GoLook>.SetServer.Update(model);
                    }
                }
                else
                {
                    List<int> result = new List<string>(ids.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries)).ConvertAll(int.Parse);
                    var list = OperateContext<lv_GoLook>.SetServer.GetList(m => result.Contains(m.ID), m => m.UpdateTime, false);
                    foreach (var item in list)
                    {
                        item.IsSpecial = !item.IsSpecial;
                        OperateContext<lv_GoLook>.SetServer.Update(item);
                    }
                }
            }
            catch (Exception ex)
            {
                jsonM.Status = "err";
                jsonM.Msg = "修改数据发生错误！ 消息：" + ex.Message;
                LogHelper.WriteLog(typeof(FytGoLookController), ex);
            }
            return Json(jsonM);
        }
        #endregion

        #region 7、[去看看管理] 去看看修改审核 + ChangeAudit()
        /// <summary>
        /// 去看看修改审核
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public ActionResult ChangeAudit()
        {
            var jsonM = new JsonHelper.JsonAjaxModel() { Status = "y", Msg = "success" };
            try
            {
                var ids = FytRequest.GetFormString("id");
                var status = FytRequest.GetFormInt("status");
                if (!ids.Contains(","))
                {
                    var id = Convert.ToInt32(ids);
                    var model = OperateContext<lv_GoLook>.SetServer.GetModel(m => m.ID == id);
                    if (model != null)
                    {
                        model.Audit = status == 1 ? model.Audit = 1 : model.Audit = 2;
                        OperateContext<lv_GoLook>.SetServer.Update(model);
                    }
                }
                else
                {
                    List<int> result = new List<string>(ids.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries)).ConvertAll(int.Parse);
                    var list = OperateContext<lv_GoLook>.SetServer.GetList(m => result.Contains(m.ID), m => m.UpdateTime, false);
                    foreach (var item in list)
                    {
                        item.Audit = status == 1 ? item.Audit = 1 : item.Audit = 2;
                        OperateContext<lv_GoLook>.SetServer.Update(item);
                    }
                }
            }
            catch (Exception ex)
            {
                jsonM.Status = "err";
                jsonM.Msg = "修改数据发生错误！ 消息：" + ex.Message;
                LogHelper.WriteLog(typeof(FytGoLookController), ex);
            }
            return Json(jsonM);
        }
        #endregion

    }
}