function BinderView() {

    var DH = DirectoriesHandler();
    const uri = 'api/Directories';

    function parseDirectoryContent(directory) {
        directory = "d:\\Sources\\WebGallery\\TestPictures\\Duha\\";
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
        var $table = $("#RightTable");
        for (let i = 0; i < arrayOfBinders.length; i++) {
            addHashedElementDtoInfoRow($table, arrayOfBinders[i]);
        }
    }

    function showContentInfos(arrayOfContentInfos) {
        if (Commons.emptyArray(arrayOfContentInfos)) return;
        var $table = $("#RightTable");
        for (let i = 0; i < arrayOfContentInfos.length; i++) {
            addHashedElementDtoInfoRow($table, arrayOfContentInfos[i]);
        }
    }


    function addHashedElementDtoInfoRow($table, dto) {
        var $tr = $table.find('tbody:last').append('<tr>');
        $tr.append('<td>' + dto.hash + '</td>');
        $tr.append('<td>' + dto.type + '</td>');
        $tr.append('<td>' + dto.label + '</td>');
    }

    function getImage() {
        hash = '133800811c196fce8cd5d18c6fd6a4fbb821b614';
        DH.getImage(hash)
            .done(function (data) {
                var a = 1;
            })
            .fail(function (nevim) {
                var b = 2;
                $("#MainImage").attr('src', nevim.responseText);
            });
    }

    function mainImageMaxWidthChanged(e) {
        var num = e.target.value;
        $("#MainImage").attr('width', num);

    }

    $(function () {
        $("#MainImageMaxWidthInput").on('input', mainImageMaxWidthChanged);
    });

    return {
        parseDirectoryContent: parseDirectoryContent,
        getImage: getImage,
        mainImageMaxWidthChanged: mainImageMaxWidthChanged,
    }
}

var BinderViewObject = BinderView();