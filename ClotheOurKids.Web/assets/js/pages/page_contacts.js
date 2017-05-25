var ContactPage = function () {

    return {
        
    	//Basic Map
        initMap: function () {
			var map;
			$(document).ready(function(){
			  map = new GMaps({
				div: '#map',
				scrollwheel: false,				
				lat: 34.578304,
				lng: -87.015739,
                zoom: 17
			  });
			  
			  var marker = map.addMarker({
				lat: 34.578304,
				lng: -87.015739,
	            title: 'Clothe Our Kids of Morgan County'
		       });
			});
        },

        //Panorama Map
        initPanorama: function () {
		    var panorama;
		    $(document).ready(function(){
		      panorama = GMaps.createPanorama({
		        el: '#panorama',
		        lat: 34.578304,
		        lng: -87.015739
		      });
		    });
		}        

    };
}();