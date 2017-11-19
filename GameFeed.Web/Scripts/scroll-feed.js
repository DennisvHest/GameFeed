loadMore = function() {
    container.append('<div class="feed-loader"><i class="fa fa-cog fa-spin fa-3x fa-fw"></i></div>');
    $.post("/feed/scrollfeed", { scrollUrl: scrollUrl }, function (data) {
        $(".feed-loader").remove();
        container.append(data);
        $(window).bind('scroll', bindScroll);
    });
}

bindScroll = function() {
    if ($(window).scrollTop() + $(window).height() > $(document).height() - 100) {
        $(window).unbind('scroll');
        loadMore();
    }
}

$(window).scroll(bindScroll);