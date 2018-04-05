// DECLARE: Object in namespace
gwig.services.usercheckin = gwig.services.usercheckin || {};

// POST: User Checkin and return value
gwig.services.usercheckin.insert = function (data, onSuccess, onError) {
    var url = gwig.services.apiPrefix + 'api/checkin';
	var setting = {
		cache: false
        , contentType: "application/x-www-form-urlencoded; charset=UTF-8"
        , data: data
        , dataType: "json"
        , success: onSuccess
        , error: onError
        , type: "POST"
	}
	$.ajax(url, setting);
}

// GET: UserCheckin By User Id and Place Id
gwig.services.usercheckin.getByUserIdPlacesId = function (placesId, data, onSuccess, onError) {
    var url = gwig.services.apiPrefix + 'api/checkin/' + placesId;

    var setting = {
        cache: false
        , contentType: "application/x-www-form-urlencoded; charset=UTF-8"
        , data: data
        , dataType: "json"
        , success: onSuccess
        , error: onError
        , type: "GET"
    }
    $.ajax(url, setting);
}