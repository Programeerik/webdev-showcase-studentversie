class GDPR {

    constructor() {
        this.showContent();
        this.bindEvents();

        if(this.cookieStatus() !== 'accept') this.showGDPR();
    }

    bindEvents() {
        let buttonAccept = document.querySelector('.gdpr-consent__button--accept');
        
        buttonAccept.addEventListener('click', () => {
            this.cookieStatus('accept');
            this.showContent();
            this.hideRejectMessage();
            this.hideGDPR();
        });

        let buttonReject = document.querySelector('.gdpr-consent__button--reject')
        buttonReject.addEventListener('click', () => {
            this.cookieStatus('reject');
            this.resetContent();
            this.showRejectMessage();
            this.hideGDPR();
        })

        let buttonShowGdpr = document.querySelector('.gdpr-consent__button--show-gdpr');

        buttonShowGdpr.addEventListener('click', () => {
            this.showGDPR();
        });

    }

    showContent() {
        this.resetContent();
        const status = this.cookieStatus() == null ? 'not-chosen' : this.cookieStatus();
        const element = document.querySelector(`.content-gdpr-${status}`);
        element.classList.add('show-gdpr');
    }

    resetContent(){
        const classes = [
            '.content-gdpr-accept',
            '.content-gdpr-not-chosen'];
        for(const c of classes){
            document.querySelector(c).classList.add('hide');
            document.querySelector(c).classList.remove('show-gdpr');
        }
    }

    cookieStatus(status) {

        if (status) localStorage.setItem('gdpr-consent-choice', status);
        return localStorage.getItem('gdpr-consent-choice');
    }

    hideRejectMessage() {
        let e = document.querySelector('.content-gdpr-reject');
        e.classList.add('hide');
        e.classList.remove('show-gdpr');
    }

    showRejectMessage() {
        let e = document.querySelector('.content-gdpr-reject');
        e.classList.remove('hide');
        e.classList.add('show-gdpr');
    }

    hideGDPR(){
        document.querySelector(`.gdpr-consent`).classList.add('hide');
        document.querySelector(`.gdpr-consent`).classList.remove('show-gdpr');
    }

    showGDPR(){
        document.querySelector(`.gdpr-consent`).classList.add('show-gdpr');
    }

}

const gdpr = new GDPR();

