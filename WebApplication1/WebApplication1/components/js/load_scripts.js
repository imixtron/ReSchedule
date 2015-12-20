
$script(
	//CORE API
	['components/js/jquery.min.js',
	'components/api/angular.min.js'],

	function(){

		console.info('Loading Core')
		$script(
			//CORE DEPENDENCIES
			['components/api/angular-animate.min.js',
			'components/api/angular-aria.min.js',
			'components/api/angular-ui-router.min.js'],

			function(){

				console.info('Loading Core Dependencies')
				$script(
					//CONTROLLERS + DESIGN RELATED
					[
					'app.js',

					'controllers/home.js',
					'controllers/profile.js',
					'controllers/resources.js',
					
					'filters/resFilters.js',

					'components/js/ripples.min.js',
					'components/js/date.js',
					// 'components/js/dropdown.js',
					'components/js/wNumb.js',
					'components/js/material.min.js',
					'components/js/bootstrap.min.js',
					'components/js/nouislider.min.js',
					'components/js/jquery.mCustomScrollbar.concat.min.js'
				], 

				function(){

					console.info('Bootstraping App')
					angular.bootstrap(document,['mod']);
					setTimeout(function(){
						$.material.init();

					    (function($){
					        $(window).load(function(){
					            $(".content").mCustomScrollbar();
					        });
					    })(jQuery);

					},500)

				});
			})		
	});
