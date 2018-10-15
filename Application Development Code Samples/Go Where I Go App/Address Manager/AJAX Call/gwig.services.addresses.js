// DECLARE: Object in namespace
gwig.services.addresses = gwig.services.addresses || {};

// POST: Address
gwig.services.addresses.apiPostAddresses = function (data, onSuccess, onError) {
    var url = gwig.services.apiPrefix + "api/addresses/";

    var settings = {
        cache: false
        , contentType: "application/x-www-form-urlencoded; charset=UTF-8"
        , data: data
        , type: "POST"
        , datatype: "json"
        , success: onSuccess
        , error: onError
    };

    $.ajax(url, settings);

};

// UPDATE: Address by Id
gwig.services.addresses.apiPutAddress = function (id, data, onSuccess, onError) {
    var url = gwig.services.apiPrefix + "api/addresses/" + id;

    var settings = {
        cache: false
        , contentType: "application/x-www-form-urlencoded; charset=UTF-8"
        , data: data
        , type: "PUT"
        , datatype: "json"
        , success: onSuccess
        , error: onError
    };

    $.ajax(url, settings);

};

// GET: Address by Id
gwig.services.addresses.apiSelectAddress = function (id, onSuccess, onError) {
    var url = gwig.services.apiPrefix + "api/addresses/" + id;

    var settings = {
        cache: false
        , contentType: "application/x-www-form-urlencoded; charset=UTF-8"
        , type: "GET"
        , datatype: "json"
        , success: onSuccess
        , error: onError
    };

    $.ajax(url, settings);

};

// GET: All Addresses
gwig.services.addresses.apiSelectAllAddresses = function (onSuccess, onError) {
    var url = gwig.services.apiPrefix + "api/addresses/";

    var settings = {
        cache: false
        , contentType: "application/x-www-form-urlencoded; charset=UTF-8"
        , type: "GET"
        , datatype: "json"
        , success: onSuccess
        , error: onError
    };

    $.ajax(url, settings);

};

// DELETE: Address by Id
gwig.services.addresses.apiDeleteAddress = function (id, onSuccess, onError) {
    var url = gwig.services.apiPrefix + "api/addresses/" + id;

    var settings = {
        cache: false
        , contentType: "application/x-www-form-urlencoded; charset=UTF-8"
        , type: "DELETE"
        , datatype: "json"
        , success: onSuccess
        , error: onError
    };

    $.ajax(url, settings);

};
