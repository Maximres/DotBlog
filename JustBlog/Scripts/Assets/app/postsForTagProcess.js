/// <reference path="../../typings/jquery/jquery.d.ts" />
var PostList = /** @class */ (function () {
    function PostList() {
        this.posts = new Array();
    }
    PostList.prototype.load = function () {
        var _this = this;
        $.getJSON('http://localhost:21204/Home/GetUsers', function (data) {
            _this.posts = data;
            alert('данные загружены');
        });
    };
    PostList.prototype.getPath = function (str) {
        this.path = str;
    };
    PostList.prototype.processPosts = function (posts) {
        var table = '<table class="table">';
        for (var i = 0; i < this.posts.length; i++) {
            var tableRow = '<tr>' +
                '<td>' + this.posts[i].Id + '</td>' +
                '<td>' + this.posts[i].Title + '</td>' +
                '<td>' + '<input type="checkbox" name="' + this.posts[i].Title + '" value="' + this.posts[i].Id + '" checked> <td>' +
                '</tr>';
            table += tableRow;
        }
        table += '</table>';
        $('#postsTarget').html(table);
        console.log("execude");
    };
    return PostList;
}());
var Post = /** @class */ (function () {
    function Post() {
    }
    return Post;
}());
//# sourceMappingURL=postsForTagProcess.js.map