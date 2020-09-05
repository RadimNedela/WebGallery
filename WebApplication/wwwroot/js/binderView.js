function BinderView() {

    var DH = DirectoriesHandler();
    const uri = 'api/Directories';

    function parseDirectoryContent(directory) {
        directory = "d:\\Sources\\WebGallery\\TestPictures\\Chopok\\";
        DH.getDirectoryContent(directory)
            .then(function (data) {
                var binders = data.binders;
                var contentInfos = data.contentInfos;

                showBinders(binders);
                showContentInfos(contentInfos);
            });
    }


    function showBinders(arrayOfBinders) {
        if (Commons.emptyArray(arrayOfBinders)) return;
        var a = "asdf";
    }

    function showContentInfos(arrayOfContentInfos) {
        if (Commons.emptyArray(arrayOfContentInfos)) return;
        var b = "asdf";
    }

    return {
        parseDirectoryContent: parseDirectoryContent
        //getItems: getItems
    }
}

var BinderViewObject = BinderView();