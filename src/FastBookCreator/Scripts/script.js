$(function () {
    $("#jqGrid").jqGrid({
            url: "/home/GetItems",
            datatype: 'json',
            mtype: 'Get',
            colNames: ['id', 'شناسه نوع آیتم', 'صفحه', 'درس', 'عنوان', 'محتوا'],
            colModel: [
                { key: true, hidden: true, name: 'id', index: 'id', editable: true },
                { key: false, name: 'ItemTypeId', index: 'ItemTypeId', editable: true },
                 {
                     key: false,
                     name: 'PageId',
                     index: 'PageId',
                     editable: true,
                     edittype: 'select',
                     editoptions: {
                         value: {
                             '1': '1st Class',
                             '2': '2nd Class',
                             '3': '3rd Class',
                             '4': '4th Class',
                             '5': '5th Class'
                         }
                     }
                 },
                {
                    key: false,
                    name: 'LessonId',
                    index: 'LessonId',
                    editable: true,
                    edittype: 'select',
                    editoptions: { value: { 'درس اول': 1, 'درس دوم': 2, 'هیچکدام': 0 } }
                },
                { key: false, name: 'Title', index: 'Title', editable: true },
                { key: false, name: 'Content', index: 'Content', editable: true }
            ],
            pager: jQuery('#jqControls'),
            rowNum: 10,
            rowList: [10, 20, 30, 40, 50],
            height: '100%',
            viewrecords: true,
            caption: 'اطلاعات پایه ای کتاب',
            emptyrecords: 'هیچ رکوردی ثبت نشده است',
            jsonReader: {
                root: "rows",
                page: "page",
                total: "total",
                records: "records",
                repeatitems: false,
                Id: "0"
            },
            autowidth: true,
            multiselect: false
        })
        .navGrid('#jqControls', { edit: true, add: true, del: true, search: true, refresh: true },
    {
        zIndex: 100,
        url: '/home/Edit',
        closeOnEscape: true,
        closeAfterEdit: true,
        recreateForm: true,
        afterComplete: function(response) {
            if (response.responseText) {
                alert(response.responseText);
            }
        }
    },
    {
        zIndex: 100,
        url: "/home/Create",
        closeOnEscape: true,
        closeAfterAdd: true,
        afterComplete: function(response) {
            if (response.responseText) {
                alert(response.responseText);
            }
        }
    },
    {
        zIndex: 100,
        url: "/home/Delete",
        closeOnEscape: true,
        closeAfterDelete: true,
        recreateForm: true,
        msg: "Are you sure you want to delete Item ... ? ",
        afterComplete: function(response) {
            if (response.responseText) {
                alert(response.responseText);
            }
        }
    }
    ,{
        zIndex: 100,
        caption: "Search Items",
        sopt: ['fa']
    });
});


