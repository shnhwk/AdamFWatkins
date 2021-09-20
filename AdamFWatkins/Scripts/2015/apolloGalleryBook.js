function apolloGalleryBook() {
	//Set Variables
	if(typeof apolloGalleryId === 'undefined'){alert('Error: Apollo Gallery Id Missing!');}
	else {
		this.galleryOverlayId = apolloGalleryOverlayId;
		this.galleryDestinationId = apolloGalleryDestinationId;
		this.galleryPreviousId = apolloGalleryPreviousId;
		this.galleryNextId = apolloGalleryNextId;
		this.galleryCloseId = apolloGalleryCloseId;
		this.galleryMoreId = apolloGalleryMoreId;
		this.galleryLoadingId = apolloGalleryLoadingId;
		/**/
		this.galleryMenuId = apolloGalleryMenuId;
		/**/
		this.galleryId = apolloGalleryId;
		this.galleryItemsClasses = apolloGalleryItemsClasses;
		this.galleryCoverClasses = apolloGalleryCoverClasses;
		this.galleryDescriptionClasses = apolloGalleryDescriptionClasses;
		this.galleryExpandClasses = apolloGalleryExpandClasses;
		this.galleryDestinationTextClass = apolloGalleryDestinationTextClass;
		/**/
		this.galleryOverlay = $('#'+this.galleryOverlayId);
		this.galleryDestination = $('#' + this.galleryDestinationId);
		this.galleryLoading = $('#' + this.galleryLoadingId);
		this.previousButton = $('#' + this.galleryPreviousId);
		this.nextButton = $('#' + this.galleryNextId);
		this.closeButton = $('#' + this.galleryCloseId);
		this.moreButton = $('#' + this.galleryMoreId);
		/**/
		this.galleryMenu = $('#' + this.galleryMenuId);
		/**/
		this.gallery = $('#' + this.galleryId);
		this.galleryItem = this.gallery.find('.' + this.galleryItemsClasses);
		this.galleryNumber = this.galleryItem.length;
		//Check Default
		if(typeof apolloGalleryTime === 'undefined'){apolloGalleryTime = 300;}
		if(typeof apolloGalleryEffect === 'undefined'){apolloGalleryEffect = 'swing';}
		if(typeof apolloGalleryKeyboard === 'undefined'){apolloGalleryKeyboard = true;}
		if(typeof apolloGalleryHover === 'undefined'){apolloGalleryHover = 'open';}
		
		this.galleryTime = apolloGalleryTime;
		this.galleryEffect = apolloGalleryEffect;
		this.galleryKeyboard = apolloGalleryKeyboard;
		this.galleryHover = apolloGalleryHover;
		//Utility Variables
		this.galleryRange = 30;	
		this.galleryCounter = 0;	
		this.galleryMemory;
		//Start Preparation
		this.loadingTop = ($(window).height() - this.galleryLoading.height()) / 2;
		this.loadingLeft = ($(window).width() - this.galleryLoading.width()) / 2;
		this.galleryLoading.css({ top: this.loadingTop, left: this.loadingLeft });
		if (this.galleryHover == 'open') {
		    $('.' + this.galleryCoverClasses).css({ left: 50 + "%", top: 50 + "%", opacity: 0, width: 0.1 + "%", height: 0.1 + "%" });
		}
		//Buttons
		this.galleryItem.click(function (e) {
			//Standard Set
			e.preventDefault();		
			this.currentItem = $(this);
			this.previousButton = $('#' + this.galleryPreviousId);
			this.nextButton = $('#' + this.galleryNextId);
			this.galleryCounter = this.currentItem.index();
			//Check Avaliable
			if (this.currentItem.css('opacity') == '0' || this.currentItem.css('display') == 'none') {
			    if (this.galleryMemory == 'left') { this.previousButton.click(); }
			    else if (this.galleryMemory == 'right') { this.nextButton.click(); }
			}
			else {
				//Get Content
			    this.imageItem = this.currentItem.find('.' + this.galleryExpandClasses).html();
				//Start Animation				
			    this.galleryDestination.css({ opacity: 0 });
			    this.galleryDestination.html(this.imageItem);
			    this.galleryDestination.find('img').css({ width: '', height: '', margin: 0 });
			    this.galleryOverlay.stop().fadeIn(this.galleryTime);
			    this.galleryLoading.fadeIn('fast');
				setTimeout(function(){
				    this.galleryTop = ($(window).height() - this.galleryDestination.height()) / 2;
				    this.galleryDestination.css({ marginTop: this.galleryTop });
				    this.galleryLoading.stop().fadeOut('fast');
				    this.galleryDestination.stop().animate({ opacity: 1 }, this.galleryTime);
				},500);	
			}
		});
		this.previousButton.click(function (e) {
			e.stopPropagation();
			this.galleryMemory = 'left';
			if (this.galleryCounter > 0) { this.galleryCounter--; }
			else { this.galleryCounter = this.galleryNumber - 1; }
			this.galleryItem.eq(this.galleryCounter).click();
		});
		this.nextButton.click(function (e) {
			e.stopPropagation();
			this.galleryMemory = 'right';
			if (this.galleryCounter < this.galleryNumber - 1) { this.galleryCounter++; }
			else { this.galleryCounter = 0; }
			this.galleryItem.eq(this.galleryCounter).click();
		});
		this.closeButton.click(function (e) {
			e.stopPropagation();
			this.galleryOverlay.stop().fadeOut(this.galleryTime);
		});
		this.moreButton.click(function (e) {
			e.stopPropagation();
			this.current = this.galleryDestination.find('.' + this.galleryDestinationTextClass);
			if(current.css('display') == 'none') {
			    current.fadeIn(this.galleryTime);
			}
			else {
			    current.fadeOut(this.galleryTime);
			}
		});
		this.galleryOverlay.click(function () {
		    this.galleryOverlay.stop().fadeOut(this.galleryTime);
		});
		$(document).keyup(function(e) {
		    if (this.galleryOverlay.is(':visible') && this.galleryKeyboard == true) {
			  //if (e.keyCode == 13) { enter.click(); }     // enter
		        if (e.keyCode == 27) { this.galleryOverlay.click(); }   // esc
		        if (e.keyCode == 37) { this.previousButton.click(); }	//left
		        if (e.keyCode == 39) { this.nextButton.click(); }	//right
			}
		});
		//Hover
		this.galleryItem.mouseenter(function (e) {
		    if (this.galleryHover == 'open') {
				this.current = $(this);
				this.cover = current.find('.' + this.galleryCoverClasses);
				this.description = this.current.find('.' + this.galleryDescriptionClasses);
				this.cover.stop().animate({ left: 0 + '%', top: 0 + '%', opacity: 0.7, width: 100 + '%', height: 100 + '%' }, this.galleryTime, this.galleryEffect);
				this.description.stop().delay(this.galleryTime).fadeIn(this.galleryTime);
			}
		});
		this.galleryItem.mouseleave(function (e) {
		    if (this.galleryHover == 'open') {
				this.current = $(this);
				this.cover = this.current.find('.' + this.galleryCoverClasses);
				this.description = this.current.find('.' + this.galleryDescriptionClasses);
				this.description.stop().fadeOut(this.galleryTime);
				this.cover.delay(this.galleryTime).animate({ left: 50 + "%", top: 50 + "%", opacity: 0, width: 0.1 + "%", height: 0.1 + "%" }, this.galleryTime, this.galleryEffect);
			}
		});			
		//Resize	
		$(window).resize(function(){
			galleryResize();
		});		
		function galleryResize() {
		    this.galleryItem.css({ width: '', height: '' });
		    this.galleryItem.each(function () {
				this.content = $(this);
				this.contentWidth = this.content.width();
				this.contentHeight = this.contentWidth * 0.75;
				this.content.css({ height: this.contentHeight });
				this.current = cthis.ontent.find('img').eq(0);
				this.currentHeight = this.current.height();
				this.currentWidth = this.current.width();
				if (this.currentWidth >= this.currentHeight) {
				    this.current.css({ height: 100 + '%' });
				    this.currentWidth = this.current.width();
				    this.current.css({ marginLeft: ((this.contentWidth - this.currentWidth) / 2) });
				}
				else if (this.currentHeight > this.currentWidth) {
				    this.current.css({ width: 100 + '%' });
				    this.currentHeight = current.height();
				    this.current.css({ marginTop: (this.contentHeight - this.currentHeight) / 2 });
				}			
			});
		    this.galleryMenu.find('a').eq(0).click();
		}
		setTimeout(function(){
			galleryResize();
		},500);
		this.galleryAjaxEffect = true;
		if (this.galleryAjaxEffect == true) {
		    this.galleryItem.css({ opacity: 0 });
		    this.end = this.gallery.offset().top;
			this.galleryItem.each(function () {
				this.current = $(this);		
				$(window).scroll(function() {
					this.start = $(window).scrollTop()+($(window).height()*0.8);
					if (this.start > this.end && this.galleryAjaxEffect == true) {
						//current.delay(current.index()*100).animate({opacity: 1},100);
						setTimeout(function(){
						    this.current.css({ opacity: 1 });
						}, this.current.index() * 100);
					}
				});
			});
		}
	}//ELSE
}