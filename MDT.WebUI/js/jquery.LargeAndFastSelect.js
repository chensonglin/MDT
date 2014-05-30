// JavaScript Document
///////////////////////////////////////适用于大量且项相同的下拉框////////////////////////////////////////////
var initNum = 1;
;(function ($) {
    $.fn.extend({
        MadeDiv: function (options) {//创建下拉框的选项div
            option = $.extend({
                liClass: 'liClass', 	//选项li的类，默认为5
                liActive: 'liActive', //下拉选项li被选中时的状态，默认为liActive
                hideTxt: 'hidetxt',  //保存了“项”的隐藏域
                txtBox: 'txtbox'   //从此隐藏域中取出值，该值为一个文本框的id，单击选项或按上下键时将值赋给该文本框
            }, options || {})
            $(this).each(function () {
                var self = $(this); //self为当前div
                var hideTxt = option.txtBox
                var str = $("#"+option.hideTxt).val();
                var items = str.split('|');
                var lis = '';
                for (i = 0; i < items.length; i++) {
                    var arr = items[i].split('*');
                    lis += '<li style="height:18px;" class="'+option.liClass+'"><span>' + arr[1] + '</span></li>';
                }
                $('ul li', self).remove();
                $('ul', self).append(lis);
                $('li', self)
		        .bind('click', function () {
                    var textBox = $('#'+$('#'+hideTxt).val());
		            textBox.val($(this).find('span').last().text());
		        });
                var lab=0;
                $(document).bind('keydown', function (event) {
                    if(self.is(":visible")) {
                        var n = $('li:visible', self).length
                        key = event.keyCode; //下：40； 上：38； 回车：13
                        switch (key) {
                            case 40:
                                lab = lab < n ? (lab + 1) : n;
                                $($('li:visible', self)[lab - 1]).addClass(option.liActive).siblings().removeClass(option.liActive);
                                initNum = 0;
                                var textBox = $('#'+$('#'+hideTxt).val());
		                        textBox.val($($('li:visible', self)[lab-1]).find('span').last().text());
                                conScroll(lab);
                                return false;
                                break
                            case 38:
                                lab = lab > 1 ? (lab - 1) : 1;
                                $($('li:visible', self)[lab - 1]).addClass(option.liActive).siblings().removeClass(option.liActive);
                                initNum = 0;
                                var textBox = $('#'+$('#'+hideTxt).val());
		                        textBox.val($($('li:visible', self)[lab-1]).find('span').last().text());
                                conScroll(lab);
                                return false
                                break
                            case 13:
                                return false;
                            default:
                                return true;
                        }
                    }
                }).bind('keyup', function (event) {
                     if(self.is(":visible")) {
                        var n = $('li:visible', self).length
                        key = event.keyCode; //下：40； 上：38； 回车：13
                        if (key == 13) { conHide(); }
                    }
                });
                function conScroll(i) {	//滚动条上下移动函数
                    var total = Math.floor(self.height() / $('li', self).height());
                    if (i >= total)
                        self.scrollTop((i - total) * $('li', self).height());
                    else
                        self.scrollTop(0);
                }
                /*------------------下拉选项隐藏函数-------------------*/
                function conHide() {	//下拉选项隐藏函数
                    lab = 0;
                    self.hide();
                    $(document).unbind('click', conHide);
                }
             }); //each 结束
            return this;
        }
    });
})(jQuery)
; (function ($) {
    $.fn.extend({
        ShowDivAndSelect: function (options) {
            op = $.extend({
                liActive: 'liActive', //下拉选项li被选中时的状态，默认为liActive
                displayDiv: 'div',
                hideTxtBox: 'hide'
            }, options || {})
            $(this).each(function () {	//each 开始
                var self = $(this); //self为当前input
                var textBoxId = this.id;
                var liActive = op.liActive;
                var lab = 0; 		//初始化下拉选择的当前位置
                var displayDiv = op.displayDiv;
                var hideTxtBox = op.hideTxtBox;
                var parent = self.wrap('<span class="wrap" style="display:inline-block; height:21px; line-height:21px;"></span>').parent('.wrap');
                var addHtml = '<img alt="" src="/images/selectRight.png"  style="cursor:pointer;" class="imBtn" />';
                parent.append(addHtml);
                /*------------------绑定下拉按钮点击事件-------------------*/
                $('img', parent).bind('click', function () {
                    $("#"+hideTxtBox).val(textBoxId);
                    setTimeout(ShowDivToSelect);
                });

                function searchValToSelect() {
                    $("#"+hideTxtBox).val(textBoxId);
                    searchValToSelect2(self,displayDiv,liActive);
                }
                function ShowDivToSelect() {
                    ShowDivToSelect2(self,displayDiv,liActive);
                }
                /*------------------绑定oninput onpropertychange函数-------------------*/
                self.bind('change', searchValToSelect).bind('keyup', searchValToSelect);
            }); //each 结束
            return this;
        }
    });
})(jQuery)
function ShowDivToSelect2(self,displayDiv,liActive){
    var left = self.offset().left; //取得当前控件的left
    var top = self.offset().top; 	//取得当前控件的top
    var height = self.height(); 	//取得当前控件的height
    var width = self.width()+20;
    var div = $('#'+displayDiv)
    var divHeight = div.height();
    var divTop = 0;
    
    $('li', div).removeClass(liActive);
    $('li', div).removeClass('hide');
    if ((top+height+divHeight)>$(document.body).height()) {
        divTop = top-divHeight;
    }
    else {
        divTop = top + height;
    }
    div.css({
            'left': left,
            'top': divTop,
            'width':width
    });
    
    div.show();
    $(document).bind('click', conHide);
    /*------------------下拉选项隐藏函数-------------------*/
    function conHide() {	//下拉选项隐藏函数
        lab = 0;
        div.hide();
        $(document).unbind('click', conHide);
    }
}
function searchValToSelect2(self,displayDiv,liActive){
    if (0 == initNum) {
        initNum++;
        return;
    }
    var left = self.offset().left; //取得当前控件的left
    var top = self.offset().top; 	//取得当前控件的top
    var height = self.height(); 	//取得当前控件的height
    var width = self.width()+20;

    var lab = 0;
    var div = $('#'+displayDiv)
    $('li', div).removeClass(liActive);
    for (i = 0; i < $('li', div).length; i++) {
        if ($($('li', div)[i]).find('span').last().text().toLowerCase().indexOf(self.val().toLowerCase()))
            $($('li', div)[i]).addClass('hide');
        else $($('li', div)[i]).removeClass('hide');
    }
    var divHeight = div.height();
    var divTop = 0;
    if ((top+height+divHeight)>$(document.body).height()) {
        divTop = top-divHeight;
    }
    else {
        divTop = top + height;
    }
    div.css({
            'left': left,
            'top': divTop,
            'width':width
        });
    div.show();
    $(document).bind('click', conHide);
    /*------------------下拉选项隐藏函数-------------------*/
    function conHide() {	//下拉选项隐藏函数
        lab = 0;
        div.hide();
        $(document).unbind('click', conHide);
    }
}