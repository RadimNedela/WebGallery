function BinderView() {

    var DH = DirectoriesHandler();
    const uri = 'api/Directories';

    function parseDirectoryContent(directory) {
        directory = "d:\\Sources\\WebGallery\\TestPictures\\Chopok\\";
        DH.getDirectoryContent(directory)
            .then(function (data) {
                var binders = data.binders;
                var contents = data.contents;
            });
    }

    return {
        parseDirectoryContent: parseDirectoryContent
        //getItems: getItems
    }
}

var BinderViewObject = BinderView();