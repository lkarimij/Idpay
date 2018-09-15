$('.body-wrapper').each(function() {
	var link=	 $('<div class="option"><i class="option-switcher-btn fa fa-gear hidden-xs"></i><div class="option-switcher animated"><div class="option-swticher-header"><div class="option-switcher-heading">Template Options</div><div class="theme-close"><i class="fa fa-close"></i></div></div><div class="option-swticher-body"><ul class="list-unstyled color-options"><li class="theme-default theme-active" data-color="default" data-logo="default-logo"></li><li class="theme-color1" data-color="color-option1" data-logo="logo1"></li><li class="theme-color2" data-color="color-option2" data-logo="logo2"></li><li class="theme-color3" data-color="color-option3" data-logo="logo3"></li><li class="theme-color4 last" data-color="color-option4" data-logo="logo4"></li></ul><div class="row no-col-space layoutStyle"><div class="col-xs-6"><a href="javascript:void(0);" class="btn-u  btn-block active-switcher-btn wide-layout-btn">Wide</a></div><div class="col-xs-6"><a href="javascript:void(0);" class="btn-u btn-block boxed-layout-btn">Boxed</a></div></div><div class="bg-patern"><h3>Background pattern</h3><ul class="list-unstyled"><li class="pattern-default pattern-active"></li><li class="pattern1"></li><li class="pattern2"></li><li class="pattern3 last"></li><li class="pattern4"></li><li class="pattern5"></li><li class="pattern6"></li><li class="pattern7 last"></li></ul></div><div class="row no-col-space headerStyle"><div class="col-xs-6"><a href="javascript:void(0);" class="btn-u btn-block active-switcher-btn static-header">Static header</a></div><div class="col-xs-6"><a href="javascript:void(0);" class="btn-u btn-block fixed-header">Fixed header</a></div></div><div class="row no-col-space rtl-option"><div class="col-xs-6"><a href="javascript:void(0);" class="btn-u btn-block active-switcher-btn left-right">LTR</a></div><div class="col-xs-6"><a href="javascript:void(0);" class="btn-u btn-block right-left">RTL</a></div></div></div></div></div></div>');
  $('.body-wrapper').prepend(link);
});

		//option Switcher
			var panel = jQuery('.option-switcher');

			$(document).on("click", ".option-switcher-btn" , function() {
				jQuery(this).hide(100);
				jQuery('.option-switcher').addClass('fadeInRight').removeClass('fadeOutRight').show();
      });

			jQuery('.option-switcher-btn').click(function () {
				jQuery(this).hide(100);
				jQuery('.option-switcher').addClass('fadeInRight').removeClass('fadeOutRight').show();
			});

			jQuery('.theme-close').click(function () {
				jQuery('.option-switcher').removeClass('fadeInRight').addClass('fadeOutRight').hide(1000);
				jQuery('.option-switcher-btn').show(1000);
			});

			jQuery('.color-options li').click(function () {
				var color = jQuery(this).attr("data-color");
				var data_logo = jQuery(this).attr("data-logo");
				setColor(color, data_logo);
				jQuery('.color-options li').removeClass("theme-active");
				jQuery(this).addClass("theme-active");
			});

			var setColor = function (color, data_logo) {
				jQuery('#option_color').attr("href", "css/" + color + ".css");
				if(data_logo == 'logo1'){
					jQuery('.navbar-brand img').attr("src", "img/logo-green" + ".png");
				}
				else if(data_logo == 'logo2'){
					jQuery('.navbar-brand img').attr("src", "img/logo-parple" + ".png");
				}
				else if(data_logo == 'logo3'){
					jQuery('.navbar-brand img').attr("src", "img/logo-red" + ".png");
				}
				else if(data_logo == 'logo4'){
					jQuery('.navbar-brand img').attr("src", "img/logo-blue" + ".png");
				}
				else if(data_logo == 'default-logo'){
					jQuery('.navbar-brand img').attr("src", "img/logo" + ".png");
				}
			}

			//Boxed Layout
			jQuery('.boxed-layout-btn').click(function(){
				jQuery(this).addClass("active-switcher-btn");
				jQuery(".wide-layout-btn").removeClass("active-switcher-btn");
				jQuery("body").addClass("bodyColor wrapper default");
				jQuery(".bg-patern").css({"display":"block"});
			});
			jQuery('.wide-layout-btn').click(function(){
				jQuery(this).addClass("active-switcher-btn");
				jQuery(".boxed-layout-btn").removeClass("active-switcher-btn");
				jQuery("body").removeClass("bodyColor wrapper default");
				jQuery(".bg-patern").css({"display":"none"});
			});

			//Background option
			jQuery('.bg-patern li.pattern-default').click(function () {
				jQuery('.bg-patern li').removeClass("pattern-active");
				jQuery(".bg-patern li.pattern-default").addClass("pattern-active");
				jQuery("body").addClass("default")
					.removeClass("pattern-01 pattern-02 pattern-03 pattern-04 pattern-05 pattern-06 pattern-07");
			});
			jQuery('.bg-patern li.pattern1').click(function () {
				jQuery('.bg-patern li').removeClass("pattern-active");
				jQuery(".bg-patern li.pattern1").addClass("pattern-active");
				jQuery("body").addClass("pattern-01")
					.removeClass("default pattern-02 pattern-03 pattern-04 pattern-05 pattern-06 pattern-07");
			});
			jQuery('.bg-patern li.pattern2').click(function () {
				jQuery('.bg-patern li').removeClass("pattern-active");
				jQuery(".bg-patern li.pattern2").addClass("pattern-active");
				jQuery("body").addClass("pattern-02")
					.removeClass("default pattern-01 pattern-03 pattern-04 pattern-05 pattern-06 pattern-07");
			});
			jQuery('.bg-patern li.pattern3').click(function () {
				jQuery('.bg-patern li').removeClass("pattern-active");
				jQuery(".bg-patern li.pattern3").addClass("pattern-active");
				jQuery("body").addClass("pattern-03")
					.removeClass("default pattern-01 pattern-02 pattern-04 pattern-05 pattern-06 pattern-07");
			});
			jQuery('.bg-patern li.pattern4').click(function () {
				jQuery('.bg-patern li').removeClass("pattern-active");
				jQuery(".bg-patern li.pattern4").addClass("pattern-active");
				jQuery("body").addClass("pattern-04")
					.removeClass("default pattern-01 pattern-02 pattern-03 pattern-05 pattern-06 pattern-07");
			});
			jQuery('.bg-patern li.pattern5').click(function () {
				jQuery('.bg-patern li').removeClass("pattern-active");
				jQuery(".bg-patern li.pattern5").addClass("pattern-active");
				jQuery("body").addClass("pattern-05")
					.removeClass("default pattern-01 pattern-02 pattern-03 pattern-04 pattern-06 pattern-07");
			});
			jQuery('.bg-patern li.pattern6').click(function () {
				jQuery('.bg-patern li').removeClass("pattern-active");
				jQuery(".bg-patern li.pattern6").addClass("pattern-active");
				jQuery("body").addClass("pattern-06")
					.removeClass("default pattern-01 pattern-02 pattern-03 pattern-04 pattern-05 pattern-07");
			});
			jQuery('.bg-patern li.pattern7').click(function () {
				jQuery('.bg-patern li').removeClass("pattern-active");
				jQuery(".bg-patern li.pattern7").addClass("pattern-active");
				jQuery("body").addClass("pattern-07")
					.removeClass("default pattern-01 pattern-02 pattern-03 pattern-04 pattern-05 pattern-06");
			});

			//Header option
			jQuery('.fixed-header').click(function(){
				jQuery(this).addClass("active-switcher-btn");
				jQuery('.static-header').removeClass("active-switcher-btn");
				jQuery("body").removeClass("static");
			});
			jQuery('.static-header').click(function(){
				jQuery(this).addClass("active-switcher-btn");
				jQuery(".fixed-header").removeClass("active-switcher-btn");
				jQuery("body").addClass("static");
			});

			//LTR and RTL
			jQuery('.rtl-option .left-right').click(function(){
				jQuery(this).addClass("active-switcher-btn");
				jQuery(".right-left").removeClass("active-switcher-btn");
				jQuery('#bootstrap-rtl').attr("href", "");
				jQuery('#rtl_css').attr("href", "");
				jQuery("body").removeClass("rtl");
			});
			jQuery('.rtl-option .right-left').click(function(){
				jQuery(this).addClass("active-switcher-btn");
				jQuery(".left-right").removeClass("active-switcher-btn");
				jQuery('#bootstrap-rtl').attr("href", "plugins/bootstrap-rtl/dist/css/bootstrap-rtl.min.css");
				jQuery('#rtl_css').attr("href", "css/rtl.css");
				jQuery("body").addClass("rtl");
			});
