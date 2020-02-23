$(document).ready(function () {
    'use strict';

    /*---------------  navbar  ------------------*/
    $('.navbar-toggle').on('click', function () {
        var wh = $(window).height();
        $('.wrapper-body').css('max-height', wh);
        $('.navbar').find('.nav-menu').addClass('open');
        $('.overlay').addClass('overlay-open');
    });
    $('.close').on('click', function () {
        $('.wrapper-body').css('max-height', 'inherit');
        $('.navbar').find('.nav-menu').removeClass('open');
        $('.overlay').removeClass('overlay-open');
        $('.search-box-top').removeClass('open-search');
    });

    /*--------------- pretty photo ------------------*/
    $('a.zoom').prettyPhoto();


    /*---------------  search box ------------------*/
    $('.search-box-tablet').on('click', function () {
        $('.search-box-top').toggleClass('open-search');
    });


    /*--------------- header fixed to top ------------------*/
    $(window).on('scroll', function () {
        var movableHeight = ($('.header-top').height());
        if ($(window).scrollTop() > movableHeight) {
            $('.menu').addClass('menu-fixed');
            $('.button-wrapper').addClass('top-content');
        }
        else {
            $('.menu').removeClass('menu-fixed');
            $('.button-wrapper').removeClass('top-content');
        }
    });
    /*---------------event others height ------------------*/
    function eventcontrol() {
        if ($(window).width() >= 480) {
            var eventHeight = $('.event-other .counter-wrap .figcaption').outerHeight();
            $('.event-other .counter-wrap .figure').css('height', eventHeight);
        }
    }
    eventcontrol();
    function eventscontrol() {
        if ($(window).width() >= 480) {
            var eventHeight = $('section.event .left-fig').outerHeight();
            $('section.event .right-fig').css('height', eventHeight);
        }
    }
    eventscontrol();
    /*--------------- Dropdown  ------------------*/
    function dropdwn() {
        if ($(window).width() <= 992) {
            $('.dropdown').on('click', function () {
                $('.menu-bar li .submenu').toggleClass('add-submenu');
            });
        }
    }
    dropdwn();
    $(window).on('resize', function () {
        dropdwn();
        eventcontrol();
        eventscontrol();
    });

    /*--------------- RESIZE FUNCTION  ------------------*/
    $(window).on('resize', function () {
        if ($(window).width() >= 992) { wayPoint(); }
    });
    if ($(window).width() >= 992) { wayPoint(); }


    /*--------------- WAYPOINt ------------------*/
    function wayPoint() {
        $('.event').waypoint(function () {
            setTimeout(function () {
                $('.event').addClass('animated flipInX');
            }, 100);

        }, { offset: '70%' });
        $('.s-header .button-wrapper').waypoint(function () {
            setTimeout(function () {
                $('.btn-content').addClass('animated fadeInUp');
            }, 100);
        }, { offset: '70%' });

        $('.about').waypoint(function () {
            setTimeout(function () {
                $('section.about .figure:nth-of-type(1)').addClass('animated fadeInLeft');
                $('section.about .figure:nth-of-type(2)').addClass('animated fadeInUp');
                $('section.about .figure:nth-of-type(3)').addClass('animated fadeInRight');

            }, 100);

        }, { offset: '70%' });

        $('.upcoming-event').waypoint(function () {
            setTimeout(function () {
                $('section.upcoming-event .figure .item-fig ').addClass('animated fadeInDown');
                $('section.upcoming-event .figure .item-content ').addClass('animated fadeInUp');

            }, 100);
        }, { offset: '70%' });

        $('header').waypoint(function () {
            setTimeout(function () {
                $('section.aboutus .figure .figcaption').addClass('animated fadeInRight');
                $('section.aboutus .figure .fig').addClass('animated fadeInLeft');
            }, 100);
        }, { offset: '70%' });

        $('section.quotation').waypoint(function () {
            setTimeout(function () {
                $('section.quotation h5').addClass('animated fadeInUp');
                $('section.quotation p').addClass('animated fadeInUp');
            }, 100);
        }, { offset: '70%' });

        $('section.staff').waypoint(function () {
            setTimeout(function () {
                $('section.staff .figure:nth-of-type(1)').addClass('animated fadeInLeft');
                $('section.staff .figure:nth-of-type(2)').addClass('animated fadeInDown');
                $('section.staff .figure:nth-of-type(3)').addClass('animated fadeInRight');
            }, 100);
        }, { offset: '70%' });
    }

    /*--------------- isotope  ------------------*/
    $(window).on('load', function () {
        $('.menu-wrapper a').on('click', function () {
            $(this).closest('.menu-wrapper').find('a').removeClass('selected');
            $(this).addClass('selected');
        });
        var $container = jQuery('#list-item');
        $container.isotope({
            itemSelector: '.figure',
            animationOptions: {
                duration: 750,
                easing: 'linear',
                queue: false
            }
        });
        $('.menu-wrapper  li a').on('click', function (e) {
            e.preventDefault();
            var filterValue = $(this).attr('data-filter');
            $container.isotope({
                filter: filterValue
            });
        });
    });


    /*--------------- loader   ------------------*/
    jQuery(window).on('load', function () {
        jQuery('.page_overlay').addClass('animated fadeOut');
        setTimeout(function () {
            jQuery('.page_overlay').hide();
        }, 500);
    });

    /*---------------- useful currency formatter ----------------*/
    /* 
    decimal_sep: character used as deciaml separtor, it defaults to '.' when omitted
    thousands_sep: char used as thousands separator, it defaults to ',' when omitted
    */
    Number.prototype.toMoney = function (decimals, decimal_sep, thousands_sep) {
        var n = this,
        c = isNaN(decimals) ? 2 : Math.abs(decimals), //if decimal is zero we must take it, it means user does not want to show any decimal
        d = decimal_sep || '.', //if no decimal separator is passed we use the dot as default decimal separator (we MUST use a decimal separator)

        /*
        according to [http://stackoverflow.com/questions/411352/how-best-to-determine-if-an-argument-is-not-sent-to-the-javascript-function]
        the fastest way to check for not defined parameter is to use typeof value === 'undefined' 
        rather than doing value === undefined.
        */
        t = (typeof thousands_sep === 'undefined') ? ',' : thousands_sep, //if you don't want to use a thousands separator you can pass empty string as thousands_sep value

        sign = (n < 0) ? '-' : '',

        //extracting the absolute value of the integer part of the number and converting to string
        i = parseInt(n = Math.abs(n).toFixed(c)) + '',

        j = ((j = i.length) > 3) ? j % 3 : 0;
        return sign + (j ? i.substr(0, j) + t : '') + i.substr(j).replace(/(\d{3})(?=\d)/g, "$1" + t) + (c ? d + Math.abs(n - i).toFixed(c).slice(2) : '');
    }

    /*----------------------------------------------------*/
    /*	Animated Scroll To Anchor
    /*----------------------------------------------------*/
    $.fn.scrollTo = function (options) {
        var settings = {
            offset: 70,       //an integer allowing you to offset the position by a certain number of pixels. Can be negative or positive
            speed: 'slow',   //speed at which the scroll animates
            override: null,  //if you want to override the default way this plugin works, pass in the ID of the element you want to scroll through here
            easing: null //easing equation for the animation. Supports easing plugin as well (http://gsgd.co.uk/sandbox/jquery/easing/)
        };

        if (options) {
            if (options.override) {
                //if they choose to override, make sure the hash is there
                options.override = (override('#') !== -1) ? options.override : '#' + options.override;
            }
            $.extend(settings, options);
        }

        return this.each(function (i, el) {
            $(el).on('click', function (e) {
                var idToLookAt;

                if ($(el).attr('href').match(/#/) !== null) {
                    e.preventDefault();
                    idToLookAt = (settings.override) ? settings.override : $(el).attr('href');//see if the user is forcing an ID they want to use
                    //if the browser supports it, we push the hash into the pushState for better linking later

                    if (history.pushState) {

                        history.pushState(null, null, idToLookAt);

                        $('html,body').stop().animate({ scrollTop: $(idToLookAt).offset().top + settings.offset }, settings.speed, settings.easing);

                    } else {
                        //if the browser doesn't support pushState, we set the hash after the animation, which may cause issues if you use offset
                        $('html,body').stop().animate({ scrollTop: $(idToLookAt).offset().top + settings.offset }, settings.speed, settings.easing, function (e) {
                            //set the hash of the window for better linking
                            //window.location.hash = idToLookAt; //[COMMENTED OUT TO DISABLE JUMPING IN IE]
                        });
                    }
                }
            });
        });
    };

    $('.GoToHome, .GoToStore, .GoToContact').scrollTo({ speed: 1400 });

    //container.style.padding = 0;
    if (jQuery('#embed_container').length) {
        resizer();
        //attach event on resize
        window.addEventListener('resize', resizer, false);
    }

});