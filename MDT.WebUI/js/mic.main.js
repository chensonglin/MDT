//全局加载事件
$(function() {
	evete.btnOver();
    evete.eachinput();
//    evete.dateTimePicker();
});
/*===============================================*/
/*加事件*/
var evete = {
		/*自适应按钮效果*/
    btnOver: function() {
        $(".btn").hover(function() {
            $(this).css("cursor", "pointer");
            $(this).attr("class", "btnOver");
        },
        function() {
            $(this).css("cursor", "default");
            $(this).attr("class", "btn");
        });

        $(".aBtn").hover(function() {
            $(this).attr("class", "aBtnOver");
        },
        function() {
            $(this).attr("class", "aBtn");
        });
		
        $(".btnTeoWords").hover(function() {
            $(this).attr("class", "btnTwoWordsOver");
        },
        function() {
            $(this).attr("class", "btnTeoWords");
        });
    },
     /*inupt换背景*/
    eachinput: function() {
        $("input:text,input:password").focus(function() {
            this.style.cssText = "background:#E0EBC3";
        }).blur(function() {
            this.style.cssText = "background:#F6F8EA";
        });
    }//,
    /*日期时间选择器*/
//    dateTimePicker: function() {
//        $(".dateSet").dynDateTime();
//        $(".timeSet").dynDateTime({
//            showsTime: true,
//            ifFormat: "%H:%M"
//        });
//        $(".dateTimeSet").dynDateTime({
//            showsTime: true,
//            ifFormat: "%Y-%m-%d %H:%M"
//        });
//    }
}