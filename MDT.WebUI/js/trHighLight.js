;(function($) {
	$.fn.extend({
	"trHilight":function(options){
		options=$.extend({
			hoverTr:"hoverTr"
		},options);

		$('tbody tr',this).live('mouseover',function(){
		  $(this).addClass(options.hoverTr);
		});
		$('tbody tr',this).live('mouseout',function(){
		  $(this).removeClass(options.hoverTr);
		});
		},
		
	"trClick":function(options){
		options=$.extend({
			clickTr:"clickTr"
		},options);

		$('tbody tr',this).live('click',function(){
		  $(this).addClass(options.clickTr).siblings().removeClass(options.clickTr); ;
		})
		},
		
	"trOddHilight":function(options){
		options=$.extend({
			oddTr:"oddTr"
		},options);

		$('tbody tr:odd',this).addClass(options.oddTr);
		}
	});
})(jQuery);

