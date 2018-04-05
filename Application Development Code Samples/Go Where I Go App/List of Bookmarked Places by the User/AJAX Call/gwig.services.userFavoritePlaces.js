// UserFavoritePlaces - Table that deals with a users bookmark lists of "Want To Go" && "Have Been"

// DECLARE: Object in namespace
gwig.services.userFavoritePlaces = gwig.services.userFavoritePlaces || {};

// POST: UserFavoritePlaces Value
gwig.services.userFavoritePlaces.apiPostUserFavoritePlaces = function (data, onSuccess, onError) {
    var url = gwig.services.apiPrefix + "api/userfavoriteplaces/";

    var settings = {
        cache: false
        , contentType: "application/x-www-form-urlencoded; charset=UTF-8"
        , data: data
        , type: "POST"
        , datatype: "json"
        , success: onSuccess
        , error: onError
    };

    $.ajax(url, settings)

}

// GET: UserFavoritePlaces Value by UserFavoritePlace ID
gwig.services.userFavoritePlaces.apiSelectByIsUserFavoritePlaces = function (id, onSuccess, onError) {
    var url = gwig.services.apiPrefix + "api/userfavoriteplaces/" + id;

    var settings = {
        cache: false
        , contentType: "application/x-www-form-urlencoded; charset=UTF-8"
        , data: data
        , type: "GET"
        , datatype: "json"
        , success: onSuccess
        , error: onError
    };

    $.ajax(url, settings)
}

// GET: UserFavoritePlaces Value by User ID
gwig.services.userFavoritePlaces.apiSelectByUserIdFavoritePlaces = function (userId, onSuccess, onError) {
    var url = gwig.services.apiPrefix + "api/userfavoriteplaces/" + userId;

    var settings = {
        cache: false
        , contentType: "application/x-www-form-urlencoded; charset=UTF-8"
        , data: data
        , type: "GET"
        , datatype: "json"
        , success: onSuccess
        , error: onError
    };

    $.ajax(url, settings)
}

// GET: UserFavoritePlaces Value by Current User ID
gwig.services.userFavoritePlaces.apiSelectByCurrentUserFavoritePlaces = function (onSuccess, onError) {
    var url = gwig.services.apiPrefix + "api/userfavoriteplaces/current";

    var settings = {
        cache: false
        , contentType: "application/x-www-form-urlencoded; charset=UTF-8"
        , type: "GET"
        , datatype: "json"
        , success: onSuccess
        , error: onError
    };

    $.ajax(url, settings)
}

// GET: UserFavoritePlaces by User ID and Place ID
gwig.services.userFavoritePlaces.apiSelectByCurrentUserIdAndPlaceId = function (userId, placeId, onSuccess, onError) {
    var url = gwig.services.apiPrefix + "api/userfavoriteplaces/list/" + placeId;

    var settings = {
        cache: false
        , contentType: "application/x-www-form-urlencoded; charset=UTF-8"
        , type: "GET"
        , datatype: "json"
        , success: onSuccess
        , error: onError
    };

    $.ajax(url, settings)
}