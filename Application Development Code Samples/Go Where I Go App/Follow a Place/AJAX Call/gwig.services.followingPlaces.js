// DECLARE: Object in namespace
gwig.services.followingplaces = gwig.services.followingplaces || {};

// POST: Add Current User as a Follower of a Single Place
gwig.services.followingplaces.Insert = function (data, onSuccess, onError) {
    var url = gwig.services.apiPrefix + "api/follow/place";

    var settings = {
        cache: false
          , contentType: "application/x-www-form-urlencoded; charset=UTF-8"
          , data: data
          , dataType: "json"
          , success: onSuccess
          , error: onError
          , type: "POST"
    };

    $.ajax(url, settings);
};

// GET: All Places being Followed in the FollowingPlaces table
gwig.services.followingplaces.GetAll = function (onSuccess, onError) {
    var url = gwig.services.apiPrefix + "api/follow/place";
    
    var setting = {
        cache: false
          , contentType: "application/x-www-form-urlencoded; charset=UTF-8"
          , dataType: "json"
          , success: onSuccess
          , error: onError
          , type: "GET"

    };

    $.ajax(url, setting);
}

// GET: All Users Following a Single Place
gwig.services.followingplaces.GetByPlacesId = function (id, onSuccess, onError) {
    var url = gwig.services.apiPrefix + "api/follow/place/" + id;

    var setting = {
        cache: false
        , contentType: "application/x-www-form-urlencoded; charset=UTF-8"
        ,dataType: "json"
        ,success: onSuccess
        ,error: onError
        ,type:"GET"
    };

    $.ajax(url,setting);
};

// GET: All the Places Current User is Following
gwig.services.followingplaces.GetDataByUserId = function (userId, onSuccess, onError) {
    var url = gwig.services.apiPrefix + "api/follow/place/" + userId;

    var setting = {
            cache: false
        , contentType: "application/x-www-form-urlencoded; charset=UTF-8"
        , dataType: "json"
        , success: onSuccess
        , error: onError
        , type: "GET"
    };

    $.ajax(url, setting);
};

// DELETE: Remove the Follow of the Current User for a Single Place
gwig.services.followingplaces.DeleteByUserId = function (data, onSuccess, onError) {
    var url = gwig.services.apiPrefix + "api/follow/place";

    var setting = {
        cache: false
        , contentType: "application/x-www-form-urlencoded; charset=UTF-8"
        , dataType: "json"
        , data: data
        , success: onSuccess
        , error: onError
        , type: "DELETE"
    };

    $.ajax(url, setting);
};

// POST: Add User as a Follower of a Place
gwig.services.followingplaces.PostByUserIdAndPlacesId = function (data, onSuccess, onError) {
    var url = gwig.services.apiPrefix + "api/follow/place/userId/placesId";

    var setting = {
        cache: false
        ,data: data
        , contentType: "application/x-www-form-urlencoded; charset=UTF-8"
        , dataType: "json"
        , success: onSuccess
        , error: onError
        , type: "POST"
    };

    $.ajax(url, setting);
}

// GET/CHECK: Current User is Following a Single Place
gwig.services.followingplaces.GetByUserIdAndPlacesId = function (data, onSuccess, onError) {
    var url = gwig.services.apiPrefix + "api/follow/place/{placesId}/{userId}";

    var setting = {
        cache: false
        ,data: data
        , contentType: "application/x-www-form-urlencoded; charset=UTF-8"
        , dataType: "json"
        , success: onSuccess
        , error: onError
        , type: "POST"
    };

    $.ajax(url, setting);
}
