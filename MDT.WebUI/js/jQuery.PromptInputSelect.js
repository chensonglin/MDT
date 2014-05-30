// JavaScript Document
; (function ($) {
    $.fn.extend({
        PromptInputSelect: function (options) {
            op = $.extend({
                L: 0, //下拉选型的左偏移量，默认为0
                T: 0, //下拉选型的上偏移量，默认为0
                W: 0, //下拉选型的宽偏移量，默认为0
                N: 5, //上下键操作时滚动条跟随移动的选项数，默认为5
                beforehand: true, //是否预先生成下拉节点，默认为“true”
                isuppercase:false,//是否只能输入大小写 ，默认为“false”
                imBtn: 'imBtn', 	//下拉按钮的类，默认为'imBtn'
                liClass: 'liClass', 	//选项li的类，默认为5
                liActive: 'liActive', //下拉选项li被选中时的状态，默认为liActive
                liOver: 'liOver', //鼠标移动至下拉选项li上的状态，默认为liOver
                conDiv: 'conDiv', //下拉选项外围的类，默认为conDiv
                callback: function () { } //回调函数
            }, options || {})
            $(this).each(function () {	//each 开始
                var beforehand = op.beforehand;
                var isuppercase = op.isuppercase;
                var callback = op.callback;
                var conDiv = op.conDiv;
                var init = 0;
                var self = $(this); //self为当前input
                var left = self.offset().left; //取得当前控件的left
                var top = self.offset().top; 	//取得当前控件的top
                var height = self.height(); 	//取得当前控件的height
                var width = self.width(); 		//取得当前控件的width
                var lab = 0; 		//初始化下拉选择的当前位置
                var parent = self.wrap('<span class="wrap" style="display:inline-block; height:21px; line-height:21px;"></span>').parent('.wrap');
                
                var addHtml = '<input name="" type="text" class="hide"/><img alt="" src="/images/selectRight.png"  style="cursor:pointer;" class="imBtn" /><span class="clue" style="left:' + (op.L + left) + 'px;top:' + (op.T + top + height-21) + 'px;width:' + (width + op.W-25) + 'px;position: absolute;;padding-left:5px;display:none;color:#777;">未找到以输入字符开头的选项</span><div class="PIScon ' + conDiv + '" style="left:' + (op.L + left) + 'px;top:' + (op.T + top + height) + 'px;width:' + (width + op.W) + 'px;"><ul>';
                if (beforehand) {
                    var str = $("#" + self.attr('to')).val();
                    var items = str.split('|');
                    /*------------------预先生成后续节点-------------------*/
                    for (i = 0; i < items.length; i++) {
                        var arr = items[i].split('*');
                        addHtml += '<li class=' + op.liClass + '><span class="hide">' + arr[0] + '</span><span>' + arr[1] + '</span></li>';
                    }
                }
                addHtml += '</ul></div>';
                parent.append(addHtml);
                /*------------------绑定下拉按钮点击事件-------------------*/
                $('img', parent).bind('click', function () {
                    $('li', parent).removeClass('hide').removeClass(op.liActive);
                    setTimeout(conShow);
                });
                 /*------------------绑定无数据提示层事件-------------------*/                $('.clue', parent).bind('click', function () {                	$('.clue', parent).hide();                    self.focus();                    $('input.hide', parent).val('0');                });
                /*------------------绑定下拉选项li的鼠标事件-------------------*/
                function liEvents() {
                    $('li', parent)
					.bind('mouseover', function () { $(this).addClass(op.liOver); })
					.bind('mouseout', function () { $(this).removeClass(op.liOver); })
					.bind('click', function () {
					    self.val($(this).find('span').last().text());
					    $('input.hide', parent).val($(this).find('span').first().text());
					    callback();
					});
                }
                if (beforehand) liEvents();//如果beforehand为true，则在加载页面的时候就绑定选项的相关事件
                /*------------------绑定键盘事件-------------------*/
                function keyEvents(){
                    //$(document).bind('keydown', function (event) {
                    $(document).keydown(function (event) {
                        if ($('.' + conDiv + ':visible', parent).length) {
                            var n = $('li:visible', parent).length
                            key = event.keyCode; //下：40； 上：38； 回车：13
                            switch (key) {
                                case 40:
                                    lab = lab < n ? (lab + 1) : n;
                                    $($('li:visible', parent)[lab - 1]).addClass(op.liActive).siblings().removeClass(op.liActive);
                                    changeVal(lab - 1);
                                    conScroll(lab);
                                    return false;
                                    break
                                case 38:
                                    lab = lab > 1 ? (lab - 1) : 1;
                                    $($('li:visible', parent)[lab - 1]).addClass(op.liActive).siblings().removeClass(op.liActive);
                                    changeVal(lab - 1);
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
                            var n = $('li:visible', parent).length
                            key = event.keyCode; //下：40； 上：38； 回车：13
                            if (key == 13) {callback(); conHide();}
                        }
                    });
                }
                //if (beforehand) {keyEvents();console.log(8);}//如果beforehand为true，则在加载页面的时候就绑定键盘事件

                keyEvents();

                /*------------------预定义公共函数-------------------*/
                function conShow() {	//下拉选项显示函数
                    if (!beforehand) {createLi();}
                    conPosition();
                    lab = 0;
                    $('.' + conDiv, parent).show();
                    for (i = 0; i < $('li', parent).length; i++) {
                        if ($($('li', parent)[i]).find('span').last().text().toLowerCase().indexOf(self.val().toLowerCase()) == 0){
                            $($('li', parent)[i]).addClass(op.liActive);
                            conScroll(i+2);
                            break;
                        }
                    }
                    if(!$('li:visible', parent).length){                    	self.val('');                        $('input.hide', parent).val('0');                    	$('.clue', parent).show();                        $('.PIScon', parent).hide();                    }                    else {                        $('.clue', parent).hide();                        $('.PIScon', parent).show();}
                    callback();
                    $(document).bind('click', conHide);
                }
                function conHide() {	//下拉选项隐藏函数
                    removeLi();
                    init++;
                    lab = 0;
                    $('.' + conDiv, parent).hide();
                    $(document).unbind('click', conHide);
                }
                function changeVal(i) {
                    init = 1;
                    self.val($($('li:visible', parent)[i]).find('span').last().text());
                    $('input.hide', parent).val($($('li:visible', parent)[i]).find('span').first().text());
                }
                function conScroll(i) {	//滚动条上下移动函数
                    var total = Math.floor($('.'+op.conDiv,parent).height()/$('li',parent).height());
					if(i>=total)
					$('.'+op.conDiv,parent).scrollTop((i-total)*$('li',parent).height());
                    else
                    $('.'+op.conDiv,parent).scrollTop(0);
                }
                function createLi() {	//生成li的下拉选项
                    var str = $("#" + self.attr('to')).val();
                    var items = str.split('|');
                    if ($('ul li', parent).length) return;
                    var lis = '';
                    for (i = 0; i < items.length; i++) {
                        var arr = items[i].split('*');
                        lis += '<li class=' + op.liClass + '><span class="hide">' + arr[0] + '</span><span>' + arr[1] + '</span></li>';
                    }
                    $('ul', parent).append(lis);
                    liEvents();
                    //keyEvents();
                }
                function removeLi() {	//删除li的下拉选项
                    if (!beforehand)
                        $('ul li', parent).remove();
                }
                function searchVal() {
                    if (1 == init) {
                        init++;
                        return;
                    }
                    if(isuppercase) self.val(self.val().toUpperCase())
                    var textVal = "";
                    if (self.val() != "全" && self.val() != "全部") {
                        textVal = self.val().Trim();
                    }
                    
                   if (!beforehand) createLi();
                    lab = 0;
                    $('li', parent).removeClass(op.liActive);
                    for (i = 0; i < $('li', parent).length; i++) {
                        if ($($('li', parent)[i]).find('span').last().text().toLowerCase().indexOf(textVal.toLowerCase()))
                            $($('li', parent)[i]).addClass('hide');
                        else $($('li', parent)[i]).removeClass('hide');
                    }                    if($('li:visible', parent).length > 0 && textVal == $($('li:visible', parent)[0]).find('span').last().text()){                        var firstid = $($('li:visible', parent)[0]).find('span').first().text() ;                        $('input.hide', parent).val(firstid);                    }
                    else{                        $('input.hide', parent).val("0");
                    }
                   conShow();
                }
                function conPosition() {
                    left = self.offset().left; //取得当前控件的left
                    top = self.offset().top; 	//取得当前控件的top
                    $('.' + conDiv, parent).css({
                        'left': op.L + left,
                        'top': op.T + top + height
                    });
                    $('.clue', parent).css({
                        'left': op.L + left,
                        'top': op.T + top + height-21
                    });
                }
                /*------------------绑定oninput onpropertychange函数-------------------*/
                self.bind('change', searchVal).bind('keyup', searchVal);
            }); //each 结束
            return this;
        }
    });
})(jQuery)
