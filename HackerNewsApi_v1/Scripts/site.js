let articles = [];

$(function () {
    
    $("#button-search").click(function (event) {
        var searchedArticles = articles;
        var searchText = $("#text-search").val();

        searchedArticles = searchedArticles.filter(article => {
            return article.title.includes(searchText);
        });

        $('ul').empty();
        searchedArticles.forEach(article => {
            $('ul').append(getArticleMarkup(article));
        });

    });

    $.ajax({
        url: '/HackerNews',
        type: 'GET',
        dataType: 'text',
        contentType: "application/json; charset=utf-8",  
        async: true,
        success: function (data, textStatus, xhr) {

            if (!data.includes('Server error')) {
                var resultsArray = data.split(",");
                resultsArray.forEach(function (element) {
                    var articleDetails = getArticleDetail(element);
                });

                setTimeout(function () { $("#button-search").prop("disabled", false).css('opacity', 1); }, 13000);

            }
            else {
                $('ul').append('<li class="server-error">Server error. Please try again later.</li>');
            }
            
        },
        error: function (xhr, textStatus, errorThrown) {
            console.log('Error in Operation');
        }
    });
    
    $("#text-search").keyup(function (event) {
        if (event.keyCode === 13) {
            $("#button-search").click();
        }
    });
});

function getArticleDetail(id) {
    $.ajax({
        url: '/HackerNews/GetArticleDetail',
        type: 'GET',
        dataType: 'text',
        contentType: "application/json; charset=utf-8",
        async: true,
        data: { 'id': id },
        success: function (data, textStatus, xhr) {
            if (!data.includes('Server error')) {
                var jsonData = JSON.parse(data);
                articles.push(jsonData);

                $('ul').append(getArticleMarkup(jsonData));
            }
            else {
                $('ul').append('<li class="server-error">Server error. Please try again later.</li>');
            }
        },
        error: function (xhr, textStatus, errorThrown) {
            console.log('Error in Detail Operation');
        }
    });
}

function getArticleMarkup(jsonData) {
    return "<li><a href="
        + jsonData['url']
        + " target='_blank'><p><span class='title'>"
        + jsonData['title'] + "</span><br /><span class='author'>"
        + jsonData['by']
        + "</span></p></a></li>";
}

