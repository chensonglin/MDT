// JavaScript Document
; (function ($) {
    $.fn.extend({
        CustomSelect: function (options) {
            op = $.extend({
                L: 0, //下拉选型的左偏移量，默认为0
                T: 0, //下拉选型的上偏移量，默认为0
                W: 0, //下拉选型的宽偏移量，默认为0
                imBtn: 'imBtn', 	//下拉按钮的类，默认为'imBtn'
                liClass: 'liClass', 	//选项li的类，默认为5
                liActive: 'liActive', //下拉选项li被选中时的状态，默认为liActive
                liOver: 'liOver', //鼠标移动至下拉选项li上的状态，默认为liOver
                conDiv: 'conDiv', //下拉选项外围的类，默认为conDiv
                callback: function () { } //回调函数
            }, options || {})
            $(this).each(function () {	//each 开始
                var callback = op.callback;
                var conDiv = op.conDiv;
                var self = $(this); //self为当前input
                var left = self.offset().left; //取得当前控件的left
                var top = self.offset().top; 	//取得当前控件的top
                var height = self.height(); 	//取得当前控件的height
                var width = self.width(); 		//取得当前控件的width
                var lab = 0; 		//初始化下拉选择的当前位置
                var parent = self.wrap('<span class="wrap" style="display:inline-block; height:21px; line-height:21px;"></span>').parent('.wrap');
                var addHtml = '<img alt="" src="/images/selectRight.png"  style="cursor:pointer;" class="imBtn" /><div class="PIScon ' + conDiv + '" style="left:' + (op.L + left) + 'px;top:' + (op.T + top + height) + 'px;width:' + (width + op.W) + 'px;"><ul>';
                //从隐藏域中取出下拉选项中需要填充的值。
                var str = $("#" + self.attr('to')).val();
                var items = str.split('|');
                for (i = 0; i < items.length; i++) {
                    var arr = items[i].split('*');
                    if(arr.length>2){
                      addHtml += '<li class=' + op.liClass + '><span class="hide">' + arr[0] + '</span><span class="hide outershopid">' + arr[1] + '</span><span>' + arr[2] + '</span></li>';
                    }else{
                      addHtml += '<li class=' + op.liClass + '><span class="hide">' + arr[0] + '</span><span>' + arr[1] + '</span></li>';
                    }
                }
                addHtml += '</ul></div>';
                parent.append(addHtml);
                conPosition();
                /*------------------绑定下拉按钮点击事件-------------------*/
                $('img', parent).bind('click', function () {
                    setTimeout(conShow);
                });
                self.bind('click',function(){
                    if (self.val()== "全部") {
                        self.val("");
                    }
                    if ($('.' + conDiv + ':visible', parent).length) {
                        return;
                    }
                    setTimeout(conShow);
                });
                $('li', parent).bind('click', function () {//绑定下拉选项li的鼠标事件
                    lab = $("li",parent).index($(this));
				});
                $(document).keydown(function (event) {//绑定键盘事件
                    if ($('.' + conDiv + ':visible', parent).length) {
                        var n = $('li', parent).length
                        key = event.keyCode; //下：40； 上：38； 回车：13
                        switch (key) {
                            case 40:
                                lab = lab < n-1 ? (lab + 1) : n-1;
                                $($('li', parent)[lab]).addClass(op.liActive).siblings().removeClass(op.liActive);
                                changeVal(lab);
                                conScroll(lab);
                                return false;
                                break
                            case 38:
                                lab = lab > 0 ? (lab - 1) : 0;
                                $($('li', parent)[lab]).addClass(op.liActive).siblings().removeClass(op.liActive);
                                changeVal(lab);
                                conScroll(lab);
                                return false
                                break
                            case 13:
                                return false;
                            default:
                                return true;
                        }
                    }
                }).keyup(function (event) {
                    if ($('.' + conDiv + ':visible', parent).length) {
                        key = event.keyCode; //下：40； 上：38； 回车：13
                        if (key == 13) {  
                            conHide();
                        }
                    }
                });
                /*------------------预定义公共函数-------------------*/
                function conShow() {	//下拉选项显示函数
                    conPosition();
                    $('.' + conDiv, parent).show();
                    $('li', parent).removeClass(op.liActive);
                    for (i = 0; i < $('li', parent).length; i++) {
                        if ($($('li', parent)[i]).find('span').last().text().toLowerCase().indexOf(self.val().toLowerCase()) == 0){
                            $($('li', parent)[i]).addClass(op.liActive);
                            lab=i;
                            conScroll(i);
                            break;
                        }
                    }
                    $(document).bind('click', conHide);
                }
                function conHide() {	//下拉选项隐藏函数
                    changeVal(lab);
                    callback();
                    $('.' + conDiv, parent).hide();
                    $(document).unbind('click', conHide);
                }
                function changeVal(i) {//给文本框和隐藏id框赋值
                    self.val($($('li', parent)[i]).find('span').last().text());
                    $("#"+self.attr('txtid')).val($($('li', parent)[i]).find('span').first().text());
                    $("#"+self.attr('txtouterid')).val($($('li', parent)[i]).find('span.outershopid').first().text());
                }
                function conScroll(i) {	//滚动条上下移动函数
                    var total = Math.floor($('.' + op.conDiv, parent).height() / $('li', parent).height());
                    if ((i+3) >= total)
                        $('.' + op.conDiv, parent).scrollTop((i-total+3) * $('li', parent).height());
                    else
                        $('.' + op.conDiv, parent).scrollTop(0);
                }
                function conPosition() {
                    left = self.offset().left; //取得当前控件的left
                    top = self.offset().top; 	//取得当前控件的top
                    $('.' + conDiv, parent).css({
                        'left': op.L + left,
                        'top': op.T + top + height
                    });
                }
                /*------------------绑定keypress keyup函数-------------------*/
                self.keypress(function (event) {
                    var keyText =  String.fromCharCode(event.keyCode);
                    var textVal = "";
                    if (self.val() != "全" && self.val() != "全部") {
                        textVal = self.val().Trim()+keyText;
                    }
                    searchVal(textVal);
                }).keydown(function (event) {
                    if (event.keyCode == 8) { 
                        var textVal = self.val();
                        textVal = textVal.substring(0,textVal.length-1);
                        searchVal(textVal);
                    }
                    else {
                        return ;
                    }
                });
                function searchVal(textVal) {
                    lab = 0;
                    $('li', parent).removeClass(op.liActive);
                    if(str.toLowerCase().indexOf('*'+textVal.toLowerCase()) == -1){
                        $("#"+self.attr('txtid')).val("0");
                        $('.' + conDiv, parent).hide();
                        return false;
                    }
                    else if($('.' + conDiv + ':hidden', parent).length) {
                        conPosition();
                        $('.' + conDiv, parent).show();
                        $(document).bind('click', conHide);
                    }
                    for (i = 0; i < $('li', parent).length; i++) {
                        if ($($('li', parent)[i]).find('span').last().text().toLowerCase().indexOf(textVal.toLowerCase()) == 0){
                           lab = i;
                           $($('li', parent)[i]).addClass(op.liActive);
                           conScroll(i);
                           break;
                        }
                    }
                }
            }); //each 结束
            return this;
        }
    });
})(jQuery)
