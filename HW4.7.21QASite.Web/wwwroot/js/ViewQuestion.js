﻿$(() => {
    const questionId = $("#likes-count").data('question-id');

    $("#like-question").on("click", function () {
        $("#like-question").addClass("text-danger");
        $.post('/questions/addlike', { questionId }, function () {
       });
    })

    setInterval(function () {
        $.get('/Questions/GetLikes', { questionId }, function (likes) {
            $("#likes-count").text(likes);
        })
    }, 1000);

})