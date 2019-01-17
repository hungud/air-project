

	(function(A) {

	if (!Array.prototype.forEach)
		A.forEach = A.forEach || function(action, that) {
			for (var i = 0, l = this.length; i < l; i++)
				if (i in this)
					action.call(that, this[i], i, this);
			};

		})(Array.prototype);

	var
	mapObject,
	markers = [],
	markersData = {
		'result-map': [
			{
				id: '1',
				location_latitude: 56.130366, 
				location_longitude: -106.346771,
				/*
				image_url: 'img/hotel001.jpg',
				name_point: 'Hotel Metropolitan Tokyo',
				star_point: '<div class="rating"><i class="fa fa-star"></i><i class="fa fa-star"></i><i class="fa fa-star"></i><i class="fa fa-star-half-o"></i><i class="fa fa-star-o"></i></div>',
				location_point: 'Tokyo, Japan',
				tripadvisor_rating_point: '<i class="fa fa-certificate rated"></i><i class="fa fa-certificate rated"></i><i class="fa fa-certificate rated"></i><i class="fa fa-certificate rated"></i><i class="fa fa-certificate"></i>',
				review_count_point: '34',
				url_point: '#',
				tripadvisor_url_point: '#',
				*/
			},
			{
				id: '2',
				location_latitude: -0.023559, 
				location_longitude: 37.906193,
			},
			{
				id: '3',
				location_latitude: 40.712784, 
				location_longitude: -74.005941,
			}
		]

	};


		var mapOptions = {
			zoom: 2,
			center: new google.maps.LatLng(56.130366,-106.346771),
			mapTypeId: google.maps.MapTypeId.ROADMAP,

			mapTypeControl: false,
			mapTypeControlOptions: {
				style: google.maps.MapTypeControlStyle.DROPDOWN_MENU,
				position: google.maps.ControlPosition.LEFT_CENTER
			},
			panControl: false,
			panControlOptions: {
				position: google.maps.ControlPosition.TOP_RIGHT
			},
			zoomControl: true,
			zoomControlOptions: {
				style: google.maps.ZoomControlStyle.LARGE,
				position: google.maps.ControlPosition.TOP_RIGHT
			},
			scrollwheel: false,
			scaleControl: false,
			scaleControlOptions: {
				position: google.maps.ControlPosition.TOP_LEFT
			},
			streetViewControl: true,
			streetViewControlOptions: {
				position: google.maps.ControlPosition.LEFT_TOP
			},
			styles: [/*map styles*/]
		};
		var marker;
		
		mapObject = new google.maps.Map(document.getElementById('googleMap'), mapOptions);
		
		for (var key in markersData)
			markersData[key].forEach(function (item) {
				marker = new google.maps.Marker({
					position: new google.maps.LatLng(item.location_latitude, item.location_longitude),
					map: mapObject,
					icon: 'img/map.png',
				});

				if ('undefined' === typeof markers[key])
					markers[key] = [];
				markers[key].push(marker);
				google.maps.event.addListener(marker, 'click', (function () {
  closeInfoBox();
  getInfoBox(item).open(mapObject, this);
  mapObject.setCenter(new google.maps.LatLng(item.location_latitude, item.location_longitude));
 }));

});

	function hideAllMarkers () {
		for (var key in markers)
			markers[key].forEach(function (marker) {
				marker.setMap(null);
			});
	};

	function closeInfoBox() {
		$('div.infoBox').remove();
	};
	
	function getInfoBox(item) {
		/*
		return new InfoBox({
			content:
			'<div class="infobox-hotel-item hotel-item-grid">' +
			'<a href="' + item.url_point + '">' +
			'<div class="clearfix"></div><div class="image">' + '<img src="' + item.image_url + '" alt="Image"/>' + '<div class="view_details">+</div></div>' +
			'</a>' +
			'<figcaption>' + 
			'<a href="' + item.url_point + '">' + item.name_point +
			'</a>' + item.star_point +
			'<i class="fa fa-map-marker text-primary"></i> ' + item.location_point +
			'</figcaption>' +
			'<div class="price_block"> <div class="row">' +
			'<div class="col-xs-6">' +
			'<span class="price">$187</span>' + 
			'</div>' +
			'<div class="col-xs-6">' +
			'<div class="price_info"><div>Price for</div> <span class="text-primary">1 Night</span> for <span class="text-primary">2 Guest </span></div>' +
			'</div>' +
			'</div></div>' +
			'</div>',
			disableAutoPan: true,
			maxWidth: 0,
			pixelOffset: new google.maps.Size(-130, -365),
			isHidden: false,
			pane: 'floatPane',
			enableEventPropagation: true
		});

	*/
	};
