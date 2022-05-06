$(function () {
    $('#addIngredient').click(function (template) {
        $.get('/Recipe/IngredientEntryRow', function (template) {
            $('#recipeList').append(template);
        });
    })
})