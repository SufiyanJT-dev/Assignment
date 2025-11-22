import { ComponentFixture, TestBed } from '@angular/core/testing';

import { Popularlocation } from './popularlocation';

describe('Popularlocation', () => {
  let component: Popularlocation;
  let fixture: ComponentFixture<Popularlocation>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [Popularlocation]
    })
    .compileComponents();

    fixture = TestBed.createComponent(Popularlocation);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
