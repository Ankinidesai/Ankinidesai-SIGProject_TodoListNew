$(document).ready(function (){
    $('.ActiveCheck').change(function () {
        var self = $(this);
        var id = self.attr('id');
        var parentID = parseInt(self.attr('class').split(" ")[1]);
        var value = self.is(':checked');

        $.ajax({
            url: '/TodoDetails/AjaxEdit',
            data:
            {
                id: id,
                value: value
            },
            type: 'POST',
            success: function (result) {
                $('#tabView').html(result);
            }
        });
        //if (parentID == 0 && value)
        if (parentID == 0)
        {
            $('.' + id).each(function ()
            {
                var childid = $(this).attr('id');
                $.ajax({
                    url: '/TodoDetails/AjaxEdit',
                    data:
                    {
                        id: childid,
                        value: value
                    },
                    type: 'POST',
                    success: function (result) {
                        $('#tabView').html(result);
                    }
                });

            });
        }
        debugger;
        if (parentID > 0 && value) {
            var allChecked = true;
            $('.' + parentID).each(function () {
                if (!$(this).is(':checked') && allChecked) {
                    allChecked = false;
                }
            });
            if (allChecked) {
                $.ajax({
                    url: '/TodoDetails/AjaxEdit',
                    data:
                    {
                        id: parentID,
                        value: value
                    },
                    type: 'POST',
                    success: function (result) {
                        $('#tabView').html(result);
                    }
                });
            }
        }
    });
})