
window.swaggerUiAuth = window.swaggerUiAuth || {};
window.swaggerUiAuth.tokenName = 'id_token';
if (!window.isOpenReplaced) {
    window.open = function (open) {
        return function (url) {
            url = url.replace('response_type=token', 'response_type=token id_token');
            var response = open.call(window, url);
            return response;
        };
    }(window.open);
    window.isOpenReplaced = true;
}