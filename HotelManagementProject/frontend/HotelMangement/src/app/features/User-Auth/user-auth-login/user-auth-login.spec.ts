import { ComponentFixture, TestBed } from '@angular/core/testing';

import { UserAuthLogin } from './user-auth-login';

describe('UserAuthLogin', () => {
  let component: UserAuthLogin;
  let fixture: ComponentFixture<UserAuthLogin>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [UserAuthLogin]
    })
    .compileComponents();

    fixture = TestBed.createComponent(UserAuthLogin);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
