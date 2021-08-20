$(

    function () {

        $("#Student_firstName").autocomplete({
            source: function (request, response) {
                $.ajax({
                    url: "/Enrollments/GetStudents",
                    dataType: "json",
                    type: "POST",
                    data: {
                        term: request.term
                    },
                    success: function (data) {
                        console.log(data);
                        response($.map(data, function (item) {
                            return { label: item.name, value: item.name, id: item.id };
                        }))
                    }
                });
            },
            minLength: 2,
            select: function (event, query) {
                console.log(query);
                $("#studentID").val(query.item.id);
            }
        });







      
    }




);








