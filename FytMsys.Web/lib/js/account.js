﻿$(function () {
    $("#upHface").click(function () {
        $("#fileUrl").unbind();
        //$("#fileUrl").click();
        document.getElementById("fileUrl").click();
        $("#fileUrl").change(function () {
            upHead();
        });
    });

});
var upDoc = function () {
    var subUrl = "/Account/UpAuthDocs/?upFiles=fileUrl&isImg=1&isThum=0&isWater=0";
    $("#forms").ajaxSubmit({
        beforeSubmit: function () {
            //$(".sign-up").attr("disabled", "disabled").html(" 正在上传...");
        },
        success: function (data) {
            if (data.Status == "y") {
                $("#img").html('<img  src="' + data.Data + '" width="400" height="260" />');
            } else {
                swal('消息', data.Msg, 'error');
            }
            //$(".sign-up").attr("disabled", false).html(" 单文件上传");
            $("#fileUrl").unbind();
        },
        error: function (e) {
            //$(".sign-up").attr("disabled", false).html(" 单文件上传");
            console.log(e);
        },
        url: subUrl,
        type: "post",
        dataType: "json",
        timeout: 600000
    });
};

var upHead = function () {
    var subUrl = "/Account/UpHeadPic/?upFiles=fileUrl&isImg=1&isThum=0&isWater=0";
    $("#forms").ajaxSubmit({
        beforeSubmit: function () {
            //$(".sign-up").attr("disabled", "disabled").html(" 正在上传...");
        },
        success: function (data) {
            if (data.Status == "y") {
                $("#himg").attr("src", data.Data);
            } else {
                swal('消息', data.Msg, 'error');
            }
            //$(".sign-up").attr("disabled", false).html(" 单文件上传");
            $("#fileUrl").unbind();
        },
        error: function (e) {
            //$(".sign-up").attr("disabled", false).html(" 单文件上传");
            console.log(e);
        },
        url: subUrl,
        type: "post",
        dataType: "json",
        timeout: 600000
    });
};
