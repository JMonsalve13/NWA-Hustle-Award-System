import { HustleCardsFrontendPage } from './app.po';

describe('hustle-cards-frontend App', () => {
  let page: HustleCardsFrontendPage;

  beforeEach(() => {
    page = new HustleCardsFrontendPage();
  });

  it('should display welcome message', () => {
    page.navigateTo();
    expect(page.getParagraphText()).toEqual('Welcome to app!');
  });
});
