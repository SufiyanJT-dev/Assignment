import { ComponentFixture, TestBed } from '@angular/core/testing';

import { UserAuthSignUp } from './user-auth-sigh-up';

describe('UserAuthSignUp', () => {
  let component: UserAuthSignUp;
  let fixture: ComponentFixture<UserAuthSignUp>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [UserAuthSignUp]
    })
    .compileComponents();

    fixture = TestBed.createComponent(UserAuthSignUp);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
