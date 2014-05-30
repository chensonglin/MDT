//去除空格的函数
String.prototype.Trim = function () { return this.replace(/(^\s*)|(\s*$)/g, "") };

function show(divName, iframeName, url)  //显示隐藏层和弹出层
{
    var hideobj = document.getElementById("hidebg");
    hideobj.style.display = "block";  //显示背景层
    var hidebgHeight = document.body.scrollHeight;
    if (hidebgHeight < 1000) {
        hidebgHeight = 1000;
    }
    var hidebgWidth = document.body.scrollWidth+100;
    hideobj.style.width = hidebgWidth + "px";
    hideobj.style.height = hidebgHeight + "px";  //设置背景层的高度为当前页面高度

    var div = document.getElementById(divName);
    div.style.display = "block";  //显示弹出层
    var iTop = (window.screen.availHeight - 160 - div.offsetHeight) / 2;       //获得窗口的垂直位置;
    var iLeft = (window.screen.availWidth - 10 - div.offsetWidth) / 2;           //获得窗口的水平位置;
    if (iTop < 10) {
        iTop = 10;
    }
    div.style.top = iTop + 'px';
    div.style.left = iLeft + 'px';
    document.getElementById(iframeName).src = url;
    window.scroll(0, 0);
    document.getElementById(iframeName).focus();
    $("#" + iframeName).focus();
}
function show2(divName, iframeName, url)  //显示隐藏层和弹出层
{
    var hideobj = document.getElementById("hidebg");
    hideobj.style.display = "block";  //显示背景层
    var hidebgHeight = document.body.scrollHeight;
    if (hidebgHeight < 1000) {
        hidebgHeight = 1000;
    }
    var hidebgWidth = document.body.scrollWidth + 100;
    hideobj.style.width = hidebgWidth + "px";
    hideobj.style.height = hidebgHeight + "px";  //设置背景层的高度为当前页面高度

    var div = document.getElementById(divName);
    div.style.display = "block";  //显示弹出层
    var iTop = 20; //(window.screen.availHeight - 160 - div.offsetHeight) / 2;       //获得窗口的垂直位置;
    var iLeft = (window.screen.availWidth -120 - div.offsetWidth) / 2;           //获得窗口的水平位置;
    if (iTop < 10) {
        iTop = 10;
    }
    //div.style.top = iTop + 'px';
    div.style.left = iLeft + 'px';
    document.getElementById(iframeName).src = url;
    window.scroll(0, 0);
    document.getElementById(iframeName).focus();
    $("#" + iframeName).focus();
}

function hide(divName, iframeName)  //去除隐藏层和弹出层
{
    document.getElementById("hidebg").style.display = "none";
    document.getElementById(divName).style.display = "none";
    document.getElementById(iframeName).src = "";
}
function closeDiv2(divName, iframeName) {
    hide(divName, iframeName);
}

function closeDiv(divName, iframeName) {
    if (confirm('关闭本页面并不保存数据，是否确定？')) {
        hide(divName, iframeName);
    }
}

function closeDiv3(divName, iframeName,fun) {
    if (confirm('关闭本页面并不保存数据，是否确定？')) {
        hide(divName, iframeName);
        fun();
    }
}

String.prototype.Trim = function () { return this.replace(/(^\s*)|(\s*$)/g, "") }; //删除字符串前后空格的函数

function G(id) {
    return document.getElementById(id);
}