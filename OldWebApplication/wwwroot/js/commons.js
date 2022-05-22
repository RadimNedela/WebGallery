function CommonsConstruct() {
    function emptyArray(array) {
        if (Array.isArray(array) && array.length) {
            // array exists and is not empty
            return false;
        }
        return true;
    }

    return {
        emptyArray: emptyArray
    }
}

var Commons = CommonsConstruct();