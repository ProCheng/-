/**
 * demo.js
 * https://coidea.website
 *
 * Licensed under the MIT license.
 * http://www.opensource.org/licenses/mit-license.php
 * 
 * Copyright 2018, COIDEA
 * https://coidea.website
 */
var layer;
layui.use('layer', function () {
    layer = layui.layer;
})
$("#toggleCode").click(function () {
    $(this).attr("src","/GetCommon/checkingcodelogin?id="+Math.random())
})
  $('.modal').imagesLoaded({ background: true })
    .done( function() {
    // hide loader
    $('.loader').addClass('is-loaded');
    // demo
    CSSPlugin.defaultForce3D = false

    // init variables
    var parent = $(".modal"),
      child = $(".modal-inner"),
      monsterOpenClose = $(".monster-open-close"),
      monsterClose = $(".monster-close"),
      monsterSignup = $(".monster-signup"),
      monsterSignupSecond = $(".monster-signup-2"),
      cross = $(".close"),
      button = $("#button"),
      formHolder = $('.form'),
      form = $('form'),
      email = $("#email"),
      submitBtn = $(".submit-btn"),
      thankYou = $('.thank-you-holder'),
      emailHasValue = false,
      invisible = function() { parent.addClass('invisible'), modalTimeline.reverse() },
      modalTimeline = new TimelineMax({ paused: true, onReverseComplete: invisible }),
      closeTimeline = new TimelineMax({ paused: true }),
      signupTimelineFirst = new TimelineMax({ paused: true }),
      signupTimelineSecond = new TimelineMax({ paused: true }),
        signupTimelineFinal = new TimelineMax({ paused: true });


    // populate timeline - close (monster)
    closeTimeline
      .to(monsterClose, 0.35, { rotation: -60, xPercent: -60, ease: Expo.easeOut })
      .to(monsterClose.find('span'), 0.175, { autoAlpha: 1, x: -20, ease: Expo.easeOut }, '-=0.175')

    // populate timeline - modal
    modalTimeline
      .to(child, 0.4, { css: {top: "50%"}, ease: Expo.easeOut })
      .to(monsterOpenClose, 0.4, { css: {top: "7%"}, ease: Expo.easeInOut }, '-=0.42')
      .to(monsterOpenClose, 0.8, { css: {top: "-50%"}, ease: Expo.easeInOut }, '+=0.2')

    // populate timeline - sign up first step
    signupTimelineFirst
      .to(monsterSignup.find('span'), 0.4, { yPercent: -45, ease: Back.easeOut.config(1) })

    // populate timeline - sign up first step
    signupTimelineSecond
      .to(monsterSignup.find('span'), 0.4, { yPercent: -65, ease: Back.easeOut.config(1) })

    // populate timeline - sign up final
    signupTimelineFinal
      .to(form, 0.4, { autoAlpha: 0, y: -10, ease: Expo.easeOut })
      .to(thankYou, 0.4, { autoAlpha: 1, y: -10, ease: Expo.easeOut }, '-=0.4')
      .to(monsterSignup.find('span'), 0.4, { yPercent: 0, ease: Back.easeOut.config(1.7) }, '-=0.4')
      .to(monsterSignup.find('span'), 0.4, { autoAlpha: 0, ease: Expo.easeOut }, '-=0.4')
      .set(monsterSignup, { className: '+=submitted' })
      .set(monsterClose, { className: '+=submitted' })

    // click: reverse timeline - modal
    cross.on('click', 'span', function () {
      modalTimeline.reverse()
      closeTimeline.reverse()
    })

    // click: play timeline - modal
        button.on('click', function () {

      parent.removeClass('invisible')
      modalTimeline.play()
    })

    // hover: play/reverse timeline - close (monster)
    cross.hover(
      function(e){ closeTimeline.play() },
      function(e){ closeTimeline.reverse() }
    );

    // hover: play/reverse timeline - signup first step (monster)
    formHolder.hover(
      function(e){ signupTimelineFirst.play() },
      function(e){ signupTimelineFirst.reverse() }
    );

    // change, keyup, paste click: play/reverse timeline - signup (monster)
    email.on("change keyup paste click", function(){
      if( $(this).val() ) {
        signupTimelineSecond.play();
        emailHasValue = true;
      } else {
        signupTimelineSecond.reverse();
        emailHasValue = false;
      }
    });

    // click: play timeline - signup (monster) final
        submitBtn.on('click', function (e) {
            e.preventDefault();

            var text = $(this).val();
            signupTimelineFinal.play().delay(0);

        $.ajax({
            type: "POST",
            url: (text == "登录"?"/home/login":"/home/register"),
            data: {
                Account: $(".Account").val(),
                Pwd: $(".Pwd").val(),
                Code:$(".Code").val()
            },
            success: function (res) {
             
                
                if (res["state"]) {        
                    modalTimeline.eventCallback("onReverseComplete",  function () {
                        $('.trigger').hide();
                        layer.msg(res.msg, {
                            icon: 6,

                        }, function () {
                                window.location.replace('/home/index');
                        });

                    });
                    modalTimeline.reverse().delay(2);
                   
                } else {
                    signupTimelineFinal.eventCallback("onReverseComplete", function () {
                        layer.msg(res.msg, {
                            icon: 5,
                        });
                        //signupTimelineFinal.play()

                    })
                    signupTimelineFinal.reverse().delay(2);
                }

            },
            error: function () {
                signupTimelineFinal.eventCallback("onReverseComplete", function () {
                    layer.msg('服务器无响应，请刷新页面重试', {
                        icon: 5,
                    });
                })
                signupTimelineFinal.reverse().delay(2);
            }
        })
    
     
    })



    // simple jquery validation only for demo purposes
    validate(); $('input').on('change keyup paste', validate);

    function validate() {
      var inputsWithValues = 0;
      // get all input fields except for type='submit'
      var myInputs = $("input:not([type='submit'])");

      myInputs.each(function(e) {   
        // if it has a value, increment the counter
        if ($(this).val()) {
          inputsWithValues += 1;
        }
      });

      if (inputsWithValues == myInputs.length) {   
          $("input[type=submit]").prop("disabled", false).css("background-color", "green");
      } else {   
          $("input[type=submit]").prop("disabled", true).css("background-color", "#ff9d00");
        }
    }
    });



window.flage = true;
$("#register").on("click", function (e) {
    e.stopPropagation();
    if (!window.flage)
        return;
    window.flage = false;
    var form = $(this).parents(".group")
    var handle = (e) => {
        console.log(1)
        e.stopPropagation();
        form.removeClass("register");
        $('input[type="text"],input[type="password"]').css('font-size', '14px');
            $("input[type='text'],input[type='password']").val('');
            $("#toggleCode").click();
            $("input[type=submit]").prop("disabled", true).css("background-color", "#ff9d00");
            if ($(".diy-btn-login").val() == "登录") {
                $(".diy-btn-login").val("注册");
                $("#register").html('<span style="color:#4f4f4f">已有账号</span>，赶紧登录吧');
            }
            else {
                $(".diy-btn-login").val("登录");
                $("#register").html('<span style="color:#4f4f4f">还没有账号</span>，赶紧注册吧');

            }
        $(".group").off("transitionend", handle);
        window.flage = true;
    }
   

    $(this).parents(".group").addClass("register").on("transitionend", handle)
    
     
})


$('input[type="text"],input[type="password"]').on('input', function () {

    if ($(this).val() == "") {
        $(this).css('font-size', '14px');
        
    } else {
        $(this).css('font-size', '24px');
    }

})
