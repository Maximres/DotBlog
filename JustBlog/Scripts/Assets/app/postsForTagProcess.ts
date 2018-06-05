/// <reference path="../../typings/jquery/jquery.d.ts" />
class PostList {

    private posts: Array<Post> = new Array<Post>();
    private path: string;

    load(): void {

        $.getJSON('http://localhost:21204/Home/GetUsers',
            (data) => {
                this.posts = data;
                alert('данные загружены');

            });
    }

    getPath(str): void {
        this.path = str;
    }

    processPosts(posts: Post): void {

        var table = '<table class="table">'
        for (var i = 0; i < this.posts.length; i++) {

            var tableRow = '<tr>' +
                '<td>' + this.posts[i].Id + '</td>' +
                '<td>' + this.posts[i].Title + '</td>' +
                '<td>' + '<input type="checkbox" name="' + this.posts[i].Title + '" value="' + this.posts[i].Id +'" checked> <td>' +
                '</tr>';
            table += tableRow;
        }
        table += '</table>';
        $('#postsTarget').html(table);
        console.log("execude");
    }
}

class Post {

    Id: number;
    Title: string;
}